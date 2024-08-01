using MediatR;
using ModelLayer;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Policy
{
    public class GetPolicyByNameHandler : IRequestHandler<GetPolicyByNameQuery, List<PolicyDTO>>
    {
        private readonly IPolicyRL policyRL;

        public GetPolicyByNameHandler(IPolicyRL policyRL)
        {
            this.policyRL = policyRL;
        }

        public async Task<List<PolicyDTO>> Handle(GetPolicyByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await policyRL.GetPolicyByNameAsync(request.customername);

            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }
    }
}
