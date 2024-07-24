using ModelLayer;
using RepositoryLayer.Entity;

namespace E_InsuranceApp.Controllers
{
    public interface IEmployeeBL
    {
        Task<EmployeeEntity> CreateEmployeeAsync(EmployeeML employeeEntity);
    }
}