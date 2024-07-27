using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Policy;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Policy
{
    public class GetAllPoliciesHandler : IRequestHandler<GetAllPoliciesQuery, List<PolicyEntity>>
    {
        private readonly IPolicyRL policyRL;

        public GetAllPoliciesHandler(IPolicyRL policyRL)
        {
            this.policyRL = policyRL;
        }

        public async Task<List<PolicyEntity>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
        {
            try
            {
               return await policyRL.GetAllPoliciesAsync();
                
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }
    }
}
