using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICommissionRL
    {
        Task AddCommissionAsync(CommissionEntity commissionEntity);
        Task DeleteCommissionAsync(int agentId, int policyId);
        Task<List<CommissionEntity>> GetAllCommissionAsync();
        Task<CommissionEntity> GetByIdCommissionAsync(int agentId, int policyId);
        Task UpdateCommissionAsync(CommissionML commissionMl, float commissionPercentage);
    }
}
