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
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class EmployeeRL : IEmployeeRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService _rabbitmqService;
        private readonly ILogger<EmployeeRL> _logger;

        public EmployeeRL(EInsuranceDbContext context, RabbitMQService rabbitmqService, ILogger<EmployeeRL> logger)
        {
            _context = context;
            _rabbitmqService = rabbitmqService;
            _logger = logger;
        }

        public async Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity employee)
        {
            try
            {
                _logger.LogInformation("Creating new employee with Email: {Email}", employee.Email);

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                EmailML emailML = new EmailML
                {
                    Name = employee.Username,
                    Email = employee.Email,
                    Password = employee.Password
                };

                _rabbitmqService.SendProductMessage(emailML);

                _logger.LogInformation("Employee with Email: {Email} created successfully", employee.Email);

                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating employee with Email: {Email}", employee.Email);
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting employee with ID: {EmployeeID}", id);

                var result = await _context.Database.ExecuteSqlRawAsync("EXEC spDeleteEmployee @EmployeeID = {0}", id);

                if (result == 0)
                {
                    _logger.LogWarning("Employee not found with ID: {EmployeeID}", id);
                    throw new EmployeeException("Employee doesn't exist");
                }

                _logger.LogInformation("Employee with ID: {EmployeeID} deleted successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting employee with ID: {EmployeeID}", id);
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeeAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all employees");

                var employees = await _context.Employees
                                              .FromSqlRaw("EXEC spGetAllEmployees")
                                              .ToListAsync();

                _logger.LogInformation("Fetched all employees successfully");

                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching all employees");
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task<EmployeeEntity> GetByIdEmployeeAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching employee with ID: {EmployeeID}", id);

                var employees = await _context.Employees
                                             .FromSqlRaw("EXEC spGetEmployeeById @EmployeeID = {0}", id)
                                             .ToListAsync();
                var employee = employees.FirstOrDefault();

                if (employee == null)
                {
                    _logger.LogWarning("Employee not found with ID: {EmployeeID}", id);
                    throw new EmployeeException("Employee doesn't exist");
                }

                _logger.LogInformation("Fetched employee with ID: {EmployeeID} successfully", id);
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching employee with ID: {EmployeeID}", id);
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeEntity employeeEntity)
        {
            try
            {
                _logger.LogInformation("Updating employee with ID: {EmployeeID}", id);

                var existingEmployee = await _context.Employees
                                                     .FirstOrDefaultAsync(e => e.EmployeeID == id);
                if (existingEmployee == null)
                {
                    _logger.LogWarning("Employee not found with ID: {EmployeeID}", id);
                    throw new EmployeeException("Employee doesn't exist");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spUpdateEmployee @EmployeeID = {0}, @Username = {1}, @Password = {2}, @Email = {3}, @FullName = {4}, @Role = {5}",
                    id, employeeEntity.Username, employeeEntity.Password, employeeEntity.Email, employeeEntity.FullName, employeeEntity.Role
                );

                var emailML = new EmailML
                {
                    Name = employeeEntity.Username,
                    Email = employeeEntity.Email,
                    Password = employeeEntity.Password
                };
                _rabbitmqService.SendProductMessage(emailML);

                _logger.LogInformation("Employee with ID: {EmployeeID} updated successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating employee with ID: {EmployeeID}", id);
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
