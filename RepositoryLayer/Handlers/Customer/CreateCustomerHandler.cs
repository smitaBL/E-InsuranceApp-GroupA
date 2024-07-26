using MediatR;
using RepositoryLayer.Commands.Customer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Customer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRL customerRL;

        public CreateCustomerHandler(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
                CustomerEntity customer = new CustomerEntity()
                {
                    Username = request.username.ToLower(),
                    FullName = request.fullName.ToLower(),
                    Email = request.email.ToLower(),
                    Password = PasswordHashing.Encrypt(request.password),
                    Phone = request.phone,
                    DateOfBirth = request.dateOfBirth,
                    AgentID = request.agentID,

                };
            await  customerRL.RegisterAsync(customer);
            return Unit.Value;
        }
    }
}
