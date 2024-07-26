using BusinessLayer.Service;
using MediatR;
using ModelLayer;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Commission
{
    public class UpdateCommissionHandler:IRequestHandler<UpdateCommissionCommand>
    {
        private readonly ICommissionRL commissionRL;

        public UpdateCommissionHandler(ICommissionRL commissionRL)
        {
            this.commissionRL = commissionRL;
        }

        public async Task<Unit> Handle(UpdateCommissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CommissionML commissionMl = new CommissionML
                {
                    AgentID = request.agentID,
                    PolicyID = request.policyID,
                    CommissionAmount = request.commissionAmount,
                };
                await commissionRL.UpdateCommissionAsync(commissionMl);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }
    }
}
