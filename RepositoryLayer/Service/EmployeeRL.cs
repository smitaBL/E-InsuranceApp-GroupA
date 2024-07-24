using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
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
        
            _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            EmailML emailML = new EmailML
            {
                Name = employee.Username,
                Email = employee.Email,
                Password = PasswordHashing.Decrypt(employee.Password),
            };

            rabbitmqService.SendProductMessage(emailML);

            return employee;
        }
    }
}
