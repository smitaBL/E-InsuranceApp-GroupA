using Microsoft.Data.SqlClient;
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

namespace RepositoryLayer.Service
{
    public class CommissionRL : ICommissionRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService rabbitMQService;
        private readonly IDistributedCache _cache;

        string cacheKey = "Get_All_Commission";

        public CommissionRL(EInsuranceDbContext context, RabbitMQService rabbitMQService, IDistributedCache cache)
        {
            this._context = context;
            this.rabbitMQService = rabbitMQService;
            _cache = cache;
        }
        public async Task AddCommissionAsync(CommissionEntity commissionEntity)
        {
            try
            {
                var agent = await _context.InsuranceAgents.FirstOrDefaultAsync(x => x.AgentID == commissionEntity.AgentID);
                var policy = await _context.Policies.Include(x => x.Scheme).FirstOrDefaultAsync(x => x.PolicyID == commissionEntity.PolicyID);

                if (agent == null)
                {
                    throw new CommissionException("Invalid agent id");
                }

                if (policy == null)
                {
                    throw new CommissionException("Invalid policy id");
                }
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddCommission @AgentID = {0}, @PolicyID = {1}",
                    commissionEntity.AgentID,
                    commissionEntity.PolicyID
                );
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }

        public async Task DeleteCommissionAsync(int agentId, int policyId)
        {
            try
            {
                var commission=await _context.Commissions.FirstOrDefaultAsync(x=>x.AgentID==agentId && x.PolicyID==policyId);
                if (commission == null)
                {
                    throw new CommissionException("Invalid policy id/agent id");
                }
                await _context.Database.ExecuteSqlRawAsync("EXEC DeleteCommission @AgentID={0}, @PolicyID={1} ", agentId,policyId);
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }

        public async Task<List<CommissionEntity>> GetAllCommissionAsync()
        {
            try
            {
                var cachedCommission = RedisCacheHelper.GetFromCache<List<CommissionEntity>>(cacheKey, _cache);
                List<CommissionEntity> commissions;

                if (cachedCommission != null)
                {
                    commissions = cachedCommission.ToList();

                    if (commissions.Count != 0)
                    {
                        return commissions;
                    }
                }

                commissions = await _context.Commissions.FromSqlRaw("EXEC GetAllCommissions").ToListAsync();
                if (commissions.Count == 0)
                {
                    throw new CommissionException("No Commission Found");
                }

                RedisCacheHelper.SetToCache(cacheKey, _cache, commissions, 30, 15);
                return commissions;
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }

      

        public async Task<CommissionEntity> GetByIdCommissionAsync(int agentId, int policyId)
        {
            try
            {
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

                commission =  result.FirstOrDefault();

                if (commission == null)
                {
                    throw new CommissionException("No Commission Found");
                }

                RedisCacheHelper.SetToCache(cachedKey, _cache, commission, 30, 15);

                return commission;
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }

        public async Task UpdateCommissionAsync(CommissionML commissionML, float commissionPercentage)
        {
            try
            {
                var agent = await _context.InsuranceAgents.FirstOrDefaultAsync(x => x.AgentID == commissionML.AgentID);
                if (agent == null)
                {
                    throw new CommissionException("Invalid agent id");
                }

                var policy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyID == commissionML.PolicyID);
                if (policy == null)
                {
                    throw new CommissionException("Invalid policy id");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateCommission @AgentID={0}, @PolicyID={1}, @CommissionPercentage={2}",
                    commissionML.AgentID, commissionML.PolicyID, commissionPercentage
                );
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }
    }
}
