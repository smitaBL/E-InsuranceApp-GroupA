using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IInsurancePlanBL
    {
        public Task CreateInsurancePlan(InsurancePlanML model);
        public Task UpdateInsurancePlanByIdAsync(int id, InsurancePlanML model);
        public Task DeleteInsurancePlanByIdAsync(int id);
        public Task<List<InsurancePlanEntity>> GetAllInsurancePlanAsync();
        public Task<InsurancePlanEntity> GetInsurancePlanByIdAsync(int id);
    }
}
