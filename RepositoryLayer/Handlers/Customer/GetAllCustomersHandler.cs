using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Customer;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Customer
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerEntity>>
    {
        private readonly ICustomerRL customerRL;

        public GetAllCustomersHandler(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }

        public async Task<List<CustomerEntity>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await customerRL.GetAllCustomersAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
        }
    }
}
