using MediatR;
using RepositoryLayer.Commands.Policy;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Policy
{
    public class CreatePolicyHandler : IRequestHandler<CreatePolicyCommand>
    {
        private readonly IPolicyRL policyRL;

        public CreatePolicyHandler(IPolicyRL policyRL)
        {
            this.policyRL = policyRL;
        }

        public async Task<Unit> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                PolicyEntity policy = new PolicyEntity()
                {
                    CustomerID=request.customerID,
                    SchemeID=request.schemeID,
                    PolicyDetails=request.policyDetails,
                    Premium=request.premium,
                    DateIssued=request.dateIssued,
                    MaturityPeriod=request.maturityPeriod,
                    PolicyLapseDate=request.policyLapseDate
                };
                await policyRL.CreatePolicyAsync(policy);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }
    }
}
