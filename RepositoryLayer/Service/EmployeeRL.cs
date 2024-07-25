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
    public class EmployeeRL : IEmployeeRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService rabbitmqService;
        public EmployeeRL(EInsuranceDbContext context, RabbitMQService rabbitmqService)
        {
            this._context = context;
            this.rabbitmqService = rabbitmqService;
        }
        public async Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity employee)
        {
            try
            {
                _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                EmailML emailML = new EmailML
                {
                    Name = employee.Username,
                    Email = employee.Email,
                    Password = employee.Password
                };

                rabbitmqService.SendProductMessage(emailML);

                return employee;
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }   
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC spDeleteEmployee @EmployeeID = {0}", id);

                if (result == null)
                {
                    throw new EmployeeException("Employee doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeeAsync()
        {
            try
            {
                var employees = await _context.Employees
                                              .FromSqlRaw("EXEC spGetAllEmployees")
                                              .ToListAsync();

                return employees;
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task<EmployeeEntity> GetByIdEmployeeAsync(int id)
        {
            try
            {
                var employee = await _context.Employees
                                             .FromSqlRaw("EXEC spGetEmployeeById @EmployeeID = {0}", id)
                                             .ToListAsync();
                var result= employee.FirstOrDefault();

                if (employee == null)
                {
                    throw new EmployeeException("Employee doesn't exist");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeEntity employeeEntity)
        {
            try
            {
                var existingEmployee = await _context.Employees
                                                     .FirstOrDefaultAsync(e => e.EmployeeID == id);
                if (existingEmployee == null)
                {
                    throw new EmployeeException("Employee doesn't exist");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spUpdateEmployee @EmployeeID = {0}, @Username = {1},@Password = {2}, @Email = {3}, @FullName={4}, @Role = {5}",
                    id, employeeEntity.Username, employeeEntity.Password, employeeEntity.Email,employeeEntity.FullName,employeeEntity.Role
                );
                rabbitmqService.SendProductMessage(employeeEntity);
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
