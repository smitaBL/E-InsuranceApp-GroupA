using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IPolicyBL
    {
        Task AddPolicyAsync(int customerid,PolicyML model);
        Task DeletePolicyByIdAsync(int id);
        Task<List<PolicyEntity>> GetAllPoliciesAsync();
        Task<PolicyEntity> GetPolicyByIdAsync(int id);
        Task UpdatePolicyByIdAsync(int id,int customerid, PolicyML model);
    }
}
