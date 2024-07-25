using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAgentBL
    {
        Task CreateAgentAsync(InsuranceAgentML insuranceAgentML);
        Task DeleteAgentAsync(int id);
        Task<List<InsuranceAgentEntity>> GetAllAgentAsync();
        Task<InsuranceAgentEntity> GetByIdAgentAsync(int id);
        Task UpdateAgentAsync(int id, InsuranceAgentML insuranceAgentML);
    }
}
