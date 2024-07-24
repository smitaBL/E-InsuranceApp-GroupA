using E_InsuranceApp.Controllers;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Agent;
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
        public async Task<EmployeeEntity> CreateEmployeeAsync(EmployeeML employeeEntity)
        {
            try
            {
                return await mediator.Send(new CreateEmployeeCommand(employeeEntity.Username,employeeEntity.Password,employeeEntity.FullName,employeeEntity.Email,employeeEntity.Role));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
