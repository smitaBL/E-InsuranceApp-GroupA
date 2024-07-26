using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IPolicyRL
    {
        Task CreatePolicyAsync(PolicyEntity policy);
        Task DeletePolicyAsync(int id);
        Task<List<PolicyEntity>> GetAllPoliciesAsync();
        Task<PolicyEntity> GetPolicyByIdAsync(int id);
        Task UpdatePolicyAsync(int id, PolicyEntity policy);
    }
}
