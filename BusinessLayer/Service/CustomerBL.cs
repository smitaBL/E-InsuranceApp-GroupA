using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Commands.Customer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CustomerBL : ICustomerBL
    {
        private readonly IMediator mediator;

        public CustomerBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<CustomerEntity> RegisterAsync(CustomerML model)
        {
            try
            {
                var result = await mediator.Send(new CreateCustomerCommand(model.Username, model.FullName, model.Email, model.Password,model.Phone,model.DateOfBirth,model.AgentID ));
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
