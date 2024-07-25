using BusinessLayer.Service;
using MediatR;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Employee
{
    public class DeleteEmployeeHandler: IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRL employeeRL;

        public DeleteEmployeeHandler(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await employeeRL.DeleteEmployeeAsync(request.id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
