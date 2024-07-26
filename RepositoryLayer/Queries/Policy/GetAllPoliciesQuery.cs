using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Policy
{
    public class GetAllPoliciesQuery: IRequest<List<PolicyEntity>>
    {
    }
}
