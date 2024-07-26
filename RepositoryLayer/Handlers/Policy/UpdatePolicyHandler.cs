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
    public class UpdatePolicyHandler : IRequestHandler<UpdatePolicyCommand>
    {
        private readonly IPolicyRL policyRL;

        public UpdatePolicyHandler(IPolicyRL policyRL)
        {
            this.policyRL = policyRL;
        }

        public async Task<Unit> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                PolicyEntity policy = new PolicyEntity()
                {
                    CustomerID = request.customerID,
                    SchemeID = request.schemeID,
                    PolicyDetails = request.policyDetails,
                    Premium = request.premium,
                    DateIssued = request.dateIssued,
                    MaturityPeriod = request.maturityPeriod,
                    PolicyLapseDate = request.policyLapseDate
                };
                await policyRL.UpdatePolicyAsync(request.id,policy);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
