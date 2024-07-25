using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICustomerRL
    {
        Task DeleteCustomerByIdAsync(int id);
        Task<List<CustomerEntity>> GetAllCustomersAsync();
        Task<CustomerEntity> GetCustomerByIdAsync(int id);
        Task RegisterAsync(CustomerEntity customer);
        Task UpdateCustomerAsync(int id, CustomerEntity customer);
    }
}
