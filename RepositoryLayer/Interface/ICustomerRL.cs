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
        Task<List<CustomerEntity>> GetAllCustomersAsync();
        Task RegisterAsync(CustomerEntity customer);
    }
}
