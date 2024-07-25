using BusinessLayer.Service;
using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Employee
{
    public class UpdateEmployeeHandler:IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRL employeeRL;

        public UpdateEmployeeHandler(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                EmployeeEntity employeeEntity = new EmployeeEntity
                {
                    Username = request.username.ToLower(),
                    Password = PasswordHashing.Encrypt(request.password),
                    Email = request.email.ToLower(),
                    FullName = request.fullName.ToLower(),
                    Role = request.role.ToLower(),
                };
                await employeeRL.UpdateEmployeeAsync(request.id,employeeEntity);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
