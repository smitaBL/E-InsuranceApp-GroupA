﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Service
{
    public class CommissionRL : ICommissionRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService _rabbitMQService;
        private readonly ILogger<CommissionRL> _logger;
        private readonly IDistributedCache _cache;
        string cacheKey = "Get_All_Commission";

        public CommissionRL(EInsuranceDbContext context, RabbitMQService rabbitMQService, ILogger<CommissionRL> logger, IDistributedCache cache)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
            _logger = logger;
            _cache = cache;     
        }

        public async Task AddCommissionAsync(CommissionEntity commissionEntity)
        {
            try
            {
                _logger.LogInformation("Adding commission for AgentID: {AgentID}, PolicyID: {PolicyID}", commissionEntity.AgentID, commissionEntity.PolicyID);

                var agent = await _context.InsuranceAgents.FirstOrDefaultAsync(x => x.AgentID == commissionEntity.AgentID);
                var policy = await _context.Policies.Include(x => x.Scheme).FirstOrDefaultAsync(x => x.PolicyID == commissionEntity.PolicyID);

                if (agent == null)
                {
                    _logger.LogWarning("Invalid agent id: {AgentID}", commissionEntity.AgentID);
                    throw new CommissionException("Invalid agent id");
                }

                if (policy == null)
                {
                    _logger.LogWarning("Invalid policy id: {PolicyID}", commissionEntity.PolicyID);
                    throw new CommissionException("Invalid policy id");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddCommission @AgentID = {0}, @PolicyID = {1}",
                    commissionEntity.AgentID,
                    commissionEntity.PolicyID
                );

                _logger.LogInformation("Added commission for AgentID: {AgentID}, PolicyID: {PolicyID} successfully", commissionEntity.AgentID, commissionEntity.PolicyID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding commission for AgentID: {AgentID}, PolicyID: {PolicyID}", commissionEntity.AgentID, commissionEntity.PolicyID);
                throw new CommissionException(ex.Message);
            }
        }

        public async Task DeleteCommissionAsync(int agentId, int policyId)
        {
            try
            {
                _logger.LogInformation("Deleting commission for AgentID: {AgentID}, PolicyID: {PolicyID}", agentId, policyId);

                var commission = await _context.Commissions.FirstOrDefaultAsync(x => x.AgentID == agentId && x.PolicyID == policyId);
                if (commission == null)
                {
                    _logger.LogWarning("Invalid policy id/agent id: {AgentID}, {PolicyID}", agentId, policyId);
                    throw new CommissionException("Invalid policy id/agent id");
                }

                await _context.Database.ExecuteSqlRawAsync("EXEC DeleteCommission @AgentID={0}, @PolicyID={1} ", agentId, policyId);

                _logger.LogInformation("Deleted commission for AgentID: {AgentID}, PolicyID: {PolicyID} successfully", agentId, policyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting commission for AgentID: {AgentID}, PolicyID: {PolicyID}", agentId, policyId);
                throw new CommissionException(ex.Message);
            }
        }

        public async Task<List<CommissionEntity>> GetAllCommissionAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all commissions");

                var cachedCommission = RedisCacheHelper.GetFromCache<List<CommissionEntity>>(cacheKey, _cache);
                List<CommissionEntity> commissions;

                if (cachedCommission != null)
                {
                    commissions = cachedCommission.ToList();

                    if (commissions.Count != 0)
                    {
                        _logger.LogInformation("Fetched all commissions successfully");
                        return commissions;
                    }
                }

                commissions = await _context.Commissions.FromSqlRaw("EXEC GetAllCommissions").ToListAsync();
                if (commissions.Count == 0)
                {
                    _logger.LogError("No Commission Found");
                    throw new CommissionException("No Commission Found");
                }

                RedisCacheHelper.SetToCache(cacheKey, _cache, commissions, 30, 15);

                return commissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching all commissions");
                throw new CommissionException(ex.Message);
            }
        }

      

        public async Task<CommissionEntity> GetByIdCommissionAsync(int agentId, int policyId)
        {
            try
            {
                _logger.LogInformation("Fetching commission for AgentID: {AgentID}, PolicyID: {PolicyID}", agentId, policyId);
                string cachedKey = "Get_Commission_By_Id";
                var cachedCommission = RedisCacheHelper.GetFromCache<CommissionEntity>(cachedKey, _cache);

                CommissionEntity commission;

                if(cachedCommission != null)
                {
                    return cachedCommission;
                }

                var result = await _context.Commissions
                    .FromSqlRaw("EXEC GetCommissionById @AgentID={0}, @PolicyID={1}", agentId, policyId)
                    .ToListAsync();

                commission = result.FirstOrDefault();

                if (commission == null)
                {
                    _logger.LogWarning("Commission not found for AgentID: {AgentID}, PolicyID: {PolicyID}", agentId, policyId);
                    throw new CommissionException("Commission not found");
                }

                _logger.LogInformation("Fetched commission for AgentID: {AgentID}, PolicyID: {PolicyID} successfully", agentId, policyId);

                RedisCacheHelper.SetToCache(cachedKey, _cache, commission, 30, 15);
                return commission;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching commission for AgentID: {AgentID}, PolicyID: {PolicyID}", agentId, policyId);
                throw new CommissionException(ex.Message);
            }
        }

        public async Task UpdateCommissionAsync(CommissionML commissionML, float commissionPercentage)
        {
            try
            {
                _logger.LogInformation("Updating commission for AgentID: {AgentID}, PolicyID: {PolicyID}", commissionML.AgentID, commissionML.PolicyID);

                var agent = await _context.InsuranceAgents.FirstOrDefaultAsync(x => x.AgentID == commissionML.AgentID);
                if (agent == null)
                {
                    _logger.LogWarning("Invalid agent id: {AgentID}", commissionML.AgentID);
                    throw new CommissionException("Invalid agent id");
                }

                var policy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyID == commissionML.PolicyID);
                if (policy == null)
                {
                    _logger.LogWarning("Invalid policy id: {PolicyID}", commissionML.PolicyID);
                    throw new CommissionException("Invalid policy id");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateCommission @AgentID={0}, @PolicyID={1}, @CommissionPercentage={2}",
                    commissionML.AgentID, commissionML.PolicyID, commissionPercentage
                );

                _logger.LogInformation("Updated commission for AgentID: {AgentID}, PolicyID: {PolicyID} successfully", commissionML.AgentID, commissionML.PolicyID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating commission for AgentID: {AgentID}, PolicyID: {PolicyID}", commissionML.AgentID, commissionML.PolicyID);
                throw new CommissionException(ex.Message);
            }
        }
    }
}
