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
        private readonly RabbitMQService _rabbitMQService;
        private readonly ILogger<CustomerRL> _logger;

        public CustomerRL(EInsuranceDbContext context, RabbitMQService rabbitMQService, ILogger<CustomerRL> logger)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
            _logger = logger;
        }

        public async Task DeleteCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting customer with ID: {CustomerID}", id);

                var customer = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteCustomerById @Id={0}", id);

                if (customer == 0)
                {
                    _logger.LogWarning("Customer not found with ID: {CustomerID}", id);
                    throw new CustomerException("Customer not found");
                }

                _logger.LogInformation("Customer with ID: {CustomerID} deleted successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting customer with ID: {CustomerID}", id);
                throw new CustomerException(ex.Message);
            }
        }

        public async Task<List<CustomerEntity>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all customers");

                var customers = await _context.Customers.FromSqlRaw("EXEC sp_GetAllCustomers").ToListAsync();

                _logger.LogInformation("Fetched all customers successfully");
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching all customers");
                throw new CustomerException(ex.Message);
            }
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching customer with ID: {CustomerID}", id);

                var customers = await _context.Customers
                    .FromSqlRaw("EXEC sp_GetCustomerById @Id={0}", id)
                    .ToListAsync();
                var customer = customers.FirstOrDefault();

                if (customer == null)
                {
                    _logger.LogWarning("Customer not found with ID: {CustomerID}", id);
                    throw new CustomerException("Customer not found");
                }

                _logger.LogInformation("Fetched customer with ID: {CustomerID} successfully", id);
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching customer with ID: {CustomerID}", id);
                throw new CustomerException(ex.Message);
            }
        }

        public async Task<List<CustomerEntity>> GetCustomerByAgentIdAsync(int agentId)
        {
            try
            {
                _logger.LogInformation("Fetching customers for AgentID: {AgentID}", agentId);

                var customers = await _context.Customers
                    .FromSqlRaw("EXEC sp_GetCustomersByAgentId @AgentID = {0}", agentId)
                    .ToListAsync();

                if (!customers.Any())
                {
                    _logger.LogWarning("No customers found for AgentID: {AgentID}", agentId);
                    throw new CustomerException("No customers found for the given agent ID.");
                }

                _logger.LogInformation("Fetched customers for AgentID: {AgentID} successfully", agentId);
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching customers for AgentID: {AgentID}", agentId);
                throw new CustomerException(ex.Message);
            }
        }

        public async Task RegisterAsync(CustomerEntity customer)
        {
            try
            {
                _logger.LogInformation("Registering customer with Email: {Email}", customer.Email);

                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Email == customer.Email);
                if (customerEntity != null)
                {
                    _logger.LogWarning("Email already exists: {Email}", customer.Email);
                    throw new CustomerException("Email Id Already Exists Please Login!!");
                }

                _context.Customers.Add(customer);

                var emailML = new EmailML()
                {
                    Name = customer.FullName,
                    Email = customer.Email,
                    Password = customer.Password,
                };
                _rabbitMQService.SendProductMessage(emailML);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Customer with Email: {Email} registered successfully", customer.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while registering customer with Email: {Email}", customer.Email);
                throw new CustomerException(ex.Message);
            }
        }

        public async Task UpdateCustomerAsync(int id, CustomerEntity customer)
        {
            try
            {
                _logger.LogInformation("Updating customer with ID: {CustomerID}", id);

                var customers = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdateCustomerById @Id = {0}, @Username = {1}, @FullName = {2}, @Email = {3}, @Password = {4}, @Phone = {5}, @DateOfBirth = {6}, @AgentID = {7}",
                    id, customer.Username, customer.FullName, customer.Email, customer.Password, customer.Phone, customer.DateOfBirth, customer.AgentID
                );

                if (customers == 0)
                {
                    _logger.LogWarning("Customer not found or could not be updated with ID: {CustomerID}", id);
                    throw new CustomerException("Customer not found or could not be updated");
                }

                var emailML = new EmailML()
                {
                    Name = customer.FullName,
                    Email = customer.Email,
                    Password = customer.Password,
                };
                _rabbitMQService.SendProductMessage(emailML);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Customer with ID: {CustomerID} updated successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating customer with ID: {CustomerID}", id);
                throw new CustomerException(ex.Message);
            }
        }
    }
}
