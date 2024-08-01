using ModelLayer;
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
        Task<List<PolicyDTO>> GetAllPoliciesAsync(int customerid);
        Task<PolicyDTO> GetPolicyByIdAsync(int id);
        Task UpdatePolicyAsync(int id, PolicyEntity policy);
        Task<List<PolicyDTO>> GetPolicyByNameAsync(string customername);
    }
}
