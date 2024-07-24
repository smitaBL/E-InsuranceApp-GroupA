using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Customer
{
    public class GetAllCustomersQuery : IRequest<List<CustomerEntity>>
    {
    }
}
