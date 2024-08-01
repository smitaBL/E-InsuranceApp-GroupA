using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public async Task DeleteCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteCustomerById @Id={0}", id);

                if (customer == null)
                {
                    throw new CustomerException("Customer not found");
                }
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
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

        public async Task<CustomerEntity> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customers = await _context.Customers
                    .FromSqlRaw("EXEC sp_GetCustomerById @Id={0}", id)
                    .ToListAsync();
                var customer = customers.FirstOrDefault();

                if (customer == null)
                {
                    throw new CustomerException("Customer not found");
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
        }
        public async Task<List<CustomerEntity>> GetCustomerByAgentIdAsync(int agentid)
        {
            try
            {
                var customers = await _context.Customers
                    .FromSqlRaw("EXEC sp_GetCustomersByAgentId @AgentID = {0}", agentid)
                    .ToListAsync();

                if (!customers.Any())
                {
                    throw new Exception("No customers found for the given agent ID.");
                }

                return customers;
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

        public async Task UpdateCustomerAsync(int id, CustomerEntity customer)
        {
            try
            {
                var customers = await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateCustomerById @Id = {0}, @Username = {1}, @FullName = {2}, @Email = {3}, @Password = {4}, @Phone = {5}, @DateOfBirth = {6}, @AgentID = {7}",
                id, customer.Username, customer.FullName, customer.Email, customer.Password, customer.Phone, customer.DateOfBirth, customer.AgentID);

                if (customers == 0)
                {
                    throw new CustomerException("Customer not found or could not be updated");
                }

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
