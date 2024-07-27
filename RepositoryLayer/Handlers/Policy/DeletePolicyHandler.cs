using MediatR;
using RepositoryLayer.Commands.Policy;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Policy
{
    public class DeletePolicyHandler : IRequestHandler<DeletePolicyCommand>
    {
        private readonly IPolicyRL policyRL;

        public DeletePolicyHandler(IPolicyRL policyRL)
        {
            this.policyRL = policyRL;
        }

        public async Task<Unit> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await policyRL.DeletePolicyAsync(request.id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }
    }
}
