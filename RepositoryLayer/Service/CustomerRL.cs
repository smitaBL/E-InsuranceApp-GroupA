using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CustomerRL : ICustomerRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService rabbitMQService;

        public CustomerRL(EInsuranceDbContext context, RabbitMQService rabbitMQService)
        {
            _context = context;
            this.rabbitMQService = rabbitMQService;
        }

        public async Task<List<CustomerEntity>> GetAllCustomersAsync()
        {
            try
            {
                return await _context.Customers.FromSqlRaw("EXEC sp_GetAllCustomers").ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
        }

        public async Task RegisterAsync(CustomerEntity customer)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerID == customer.CustomerID);
                if (customerEntity != null)
                    throw new AdminException("Email Id Already Exists Please Login!!");
                _context.Customers.Add(customer);
                EmailML emailML = new EmailML()
                {
                    Name = customer.FullName,
                    Email = customer.Email,
                    Password = customer.Password,
                };
                rabbitMQService.SendProductMessage(emailML);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
        }
    }
}
