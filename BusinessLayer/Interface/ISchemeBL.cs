using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ISchemeBL
    {
        public Task CreateSchemeAsync(SchemeML model, int employeeId);
        public Task UpdateSchemeAsync(int id, SchemeML model);
        public Task DeleteSchemeAsync(int id);
        public Task<List<SchemeWithInsurancePlanML>> GetAllSchemeAsync();
        public Task<SchemeWithInsurancePlanML> GetSchemeByIdAsync(int id);
    }
}
