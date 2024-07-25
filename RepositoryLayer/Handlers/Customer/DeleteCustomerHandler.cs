using MediatR;
using RepositoryLayer.Commands.Customer;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Customer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRL customerRL;

        public DeleteCustomerHandler(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await customerRL.DeleteCustomerByIdAsync(request.id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
