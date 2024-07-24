using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICustomerBL
    {
        Task DeleteCustomerByIdAsync(int id);
        Task<List<CustomerEntity>> GetAllCustomerAsync();
        Task<CustomerEntity> GetCustomerByIdAsync(int id);
        Task RegisterAsync(CustomerML model);
        Task UpdateCustomerByIdAsync(int id, CustomerML model);
    }
}
