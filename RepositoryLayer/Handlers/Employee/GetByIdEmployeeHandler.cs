using BusinessLayer.Service;
using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Employee
{
    public class GetByIdEmployeeHandler:IRequestHandler<GetByIdEmployeeCommand,EmployeeEntity>
    {
        private readonly IEmployeeRL employeeRL;

        public GetByIdEmployeeHandler(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        public async Task<EmployeeEntity> Handle(GetByIdEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await employeeRL.GetByIdEmployeeAsync(request.id);
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
