using MediatR;
using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Policy
{
    public class GetAllPoliciesQuery : IRequest<List<PolicyDTO>>
    {
        public int customerid;

        public GetAllPoliciesQuery(int customerid)
        {
            this.customerid = customerid;
        }
    }
}
