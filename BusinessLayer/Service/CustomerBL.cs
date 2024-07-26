using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Commands.Customer;
using RepositoryLayer.Entity;
using RepositoryLayer.Queries.Customer;
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

        public async Task DeleteCustomerByIdAsync(int id)
        {
            try
            {
                await mediator.Send(new DeleteCustomerCommand(id));
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CustomerEntity>> GetAllCustomerAsync()
        {
            try
            {
                var result = await mediator.Send(new GetAllCustomersQuery());
                return result;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(int id)
        {
            try
            {
                var result = await mediator.Send(new GetCustomerByIdQuery(id));
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task RegisterAsync(CustomerML model)
        {
            try
            {
                await mediator.Send(new CreateCustomerCommand(model.Username, model.FullName, model.Email, model.Password,model.Phone,model.DateOfBirth,model.AgentID ));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCustomerByIdAsync(int id,CustomerML model)
        {
            try
            {
                await mediator.Send(new UpdateCustomerCommand(id,model.Username, model.FullName, model.Email, model.Password, model.Phone, model.DateOfBirth, model.AgentID));
            }
            catch
            {
                throw;
            }
        }
    }
}
