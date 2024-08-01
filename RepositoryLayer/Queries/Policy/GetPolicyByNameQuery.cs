using MediatR;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Policy
{
    public class GetPolicyByNameQuery : IRequest<List<PolicyDTO>>
    {
        public string customername;

        public GetPolicyByNameQuery(string customername)
        {
            this.customername = customername;
        }
    }
}
