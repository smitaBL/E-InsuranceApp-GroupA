using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IEmployeeRL
    {
        public Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity employee);
        Task DeleteEmployeeAsync(int id);
        Task<List<EmployeeEntity>> GetAllEmployeeAsync();
        Task<EmployeeEntity> GetByIdEmployeeAsync(int id);
        Task UpdateEmployeeAsync(int id, EmployeeEntity employeeEntity);
    }
}