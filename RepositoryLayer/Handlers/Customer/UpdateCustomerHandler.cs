using MediatR;
using RepositoryLayer.Commands.Customer;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Customer
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRL customerRL;

        public UpdateCustomerHandler(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CustomerEntity customer = new CustomerEntity()
                {
                    Username = request.username.ToLower(),
                    FullName = request.fullName.ToLower(),
                    Email = request.email.ToLower(),
                    Password = PasswordHashing.Encrypt(request.password),
                    Phone= request.phone,
                    DateOfBirth= request.dateOfBirth,
                    AgentID= request.agentID
                };
                await customerRL.UpdateCustomerAsync(request.id, customer);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
