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
    public class GetCustomerByAgentIdHandler : IRequestHandler<GetCustomerByAgentIdQuery, List<CustomerEntity>>
    {
        private readonly ICustomerRL customerRL;

        public GetCustomerByAgentIdHandler(ICustomerRL customerRL)
        {
            this.customerRL = customerRL;
        }
        public async Task<List<CustomerEntity>> Handle(GetCustomerByAgentIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await customerRL.GetCustomerByAgentIdAsync(request.agentid);
                return result;
            }
            catch (Exception ex)
            {
                throw new CustomerException(ex.Message);
            }
        }
    }
}
