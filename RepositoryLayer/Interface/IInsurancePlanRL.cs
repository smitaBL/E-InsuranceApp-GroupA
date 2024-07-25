using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IInsurancePlanRL
    {
        public Task CreateInsurancePlan(InsurancePlanEntity model);
        public Task UpdateInsurancePlanByIdAsync(int id, InsurancePlanEntity insurancePlanEntity);
        public Task DeleteInsurancePlanByIdAsync(int id);
        public Task<List<InsurancePlanEntity>> GetAllInsurancePlanAsync();
        public Task<InsurancePlanEntity> GetInsurancePlanByIdAsync(int id);
    }
}
