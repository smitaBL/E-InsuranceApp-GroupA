using MediatR;
using ModelLayer;
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
    public class GetPolicyByIdHandler : IRequestHandler<GetPolicyByIdQuery, PolicyDTO>
    {
        private readonly IPolicyRL policyRL;

        public GetPolicyByIdHandler(IPolicyRL policyRL)
        {
            this.policyRL = policyRL;
        }

        public async Task<PolicyDTO> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                return await policyRL.GetPolicyByIdAsync(request.id);
               
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }
    }
}
