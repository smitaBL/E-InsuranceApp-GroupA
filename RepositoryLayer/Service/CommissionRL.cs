using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public CommissionRL(EInsuranceDbContext context, RabbitMQService rabbitMQService)
        {
            this._context = context;
            this.rabbitMQService = rabbitMQService;
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
                return await _context.Commissions.FromSqlRaw("EXEC GetAllCommissions").ToListAsync();
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

                var result = await _context.Commissions
                    .FromSqlRaw("EXEC GetCommissionById @AgentID={0}, @PolicyID={1}", agentId, policyId)
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }

        public async Task UpdateCommissionAsync(CommissionML commissionMl)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateCommission @AgentID={0}, @PolicyID={1}, @CommissionAmount={2}",
                    commissionMl.AgentID, commissionMl.PolicyID, commissionMl.CommissionAmount);

            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }
    }
}
