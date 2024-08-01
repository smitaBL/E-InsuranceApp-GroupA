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
        Task<List<PolicyDTO>> GetAllPoliciesAsync(int customerid);
        Task<PolicyDTO> GetPolicyByIdAsync(int id);
        Task<List<PolicyDTO>> GetPolicyByNameAsync(string customername);
        Task UpdatePolicyByIdAsync(int id,int customerid, PolicyML model);
    }
}
