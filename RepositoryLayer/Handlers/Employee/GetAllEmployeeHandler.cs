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
    public class GetAllEmployeeHandler:IRequestHandler<GetAllEmployeeCommand,List<EmployeeEntity>>
    {
        private readonly IEmployeeRL employeeRL;

        public GetAllEmployeeHandler(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        async Task<List<EmployeeEntity>> IRequestHandler<GetAllEmployeeCommand, List<EmployeeEntity>>.Handle(GetAllEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await employeeRL.GetAllEmployeeAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
