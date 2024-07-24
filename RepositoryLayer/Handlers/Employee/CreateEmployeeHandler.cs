using BusinessLayer.Service;
using MediatR;
using RepositoryLayer.Commands.Agent;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Employee
{
    public class CreateEmployeeHandler:IRequestHandler<CreateEmployeeCommand,EmployeeEntity>
    {
        private readonly IEmployeeRL employeeRL;

        public CreateEmployeeHandler(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        public async Task<EmployeeEntity> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            EmployeeEntity employeeEntity = new EmployeeEntity
            {
                Username = request.username.ToLower(),
                FullName = request.fullName.ToLower(),
                Email = request.email.ToLower(),
                Password =PasswordHashing.Encrypt(request.password),
                Role=request.role.ToLower(),
            };
            return await employeeRL.CreateEmployeeAsync(employeeEntity);
        }
    }
}
