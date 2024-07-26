using ModelLayer;
using RepositoryLayer.Entity;

namespace E_InsuranceApp.Controllers
{
    public interface IEmployeeBL
    {
        Task CreateEmployeeAsync(EmployeeML employeeEntity);
        Task DeleteEmployeeAsync(int id);
        Task<List<EmployeeEntity>> GetAllEmployeeAsync();
        Task<EmployeeEntity> GetByIdEmployeeAsync(int id);
        Task UpdateEmployeeAsync(int id, EmployeeML employeeEntity);
    }
}