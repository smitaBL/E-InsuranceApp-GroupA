using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IEmployeeRL
    {
        public Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity employee);
    }
}