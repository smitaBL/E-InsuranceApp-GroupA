using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAgentRL
    {
        Task CreateAgentAsync(InsuranceAgentEntity insuranceAgentML);
        Task DeleteAgentAsync(int id);
        Task<List<InsuranceAgentEntity>> GetAllAgentAsync();
        Task<InsuranceAgentEntity> GetByIdAgentAsync(int id);
        Task UpdateAgentAsync(int id, InsuranceAgentEntity insuranceAgent);
    }
}
