using E_InsuranceApp.Controllers;
using MediatR;
using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IMediator mediator;
        public EmployeeBL(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task CreateEmployeeAsync(EmployeeML employeeEntity)
        {
            try
            {
                await mediator.Send(new CreateEmployeeCommand(employeeEntity.Username,employeeEntity.Password, employeeEntity.FullName, employeeEntity.Email, employeeEntity.Role));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                await mediator.Send(new DeleteEmployeeCommand(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeeAsync()
        {
            try
            {
                return await mediator.Send(new GetAllEmployeeCommand());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeEntity> GetByIdEmployeeAsync(int id)
        {
            try
            {
                return await mediator.Send(new GetByIdEmployeeCommand(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeML employeeEntity)
        {
            try
            {
                await mediator.Send(new UpdateEmployeeCommand(id, employeeEntity.Username, employeeEntity.Password, employeeEntity.Email, employeeEntity.FullName, employeeEntity.Role));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
