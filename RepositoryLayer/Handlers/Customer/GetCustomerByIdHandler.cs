using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Customer
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerEntity>
    {
        private readonly ICustomerRL customerRL;

        public GetCustomerByIdHandler(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }

        public async Task<CustomerEntity> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await customerRL.GetCustomerByIdAsync(request.id);
                return result;
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
        }
    }
}
