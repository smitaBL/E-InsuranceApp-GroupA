using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICommissionBL
    {
        Task AddCommissionAsync(CommissionML commissionML);
        Task DeleteCommissionAsync(int agentId, int policyId);
        Task<List<CommissionEntity>> GetAllCommissionAsync();
        Task<CommissionEntity> GetByIdCommissionAsync(int agentId, int policyId);
        Task UpdateCommissionAsync(CommissionML commissionML,float commissionPercent);
    }
}
