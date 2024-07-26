using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ISchemeRL
    {
        public Task CreateSchemeAsync(SchemeEntity schemeEntity, int employeeId);
        public Task UpdateSchemeAsync(int schemeId, SchemeEntity schemeEntity);
        public Task DeleteSchemeAsync(int id);
        public Task<List<SchemeWithInsurancePlanML>> GetAllSchemeAsync();
        public Task<SchemeWithInsurancePlanML> GetSchemeByIdAsync(int id);
    }
}
