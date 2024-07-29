using BusinessLayer.Service;
using MediatR;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Commission
{
    public class AddCommissionHandler:IRequestHandler<AddCommissionCommand>
    {
        private readonly ICommissionRL commissionRL;

        public AddCommissionHandler(ICommissionRL commissionRL)
        {
            this.commissionRL = commissionRL;
        }

        public async Task<Unit> Handle(AddCommissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CommissionEntity commissionMl = new CommissionEntity
                {
                    AgentID = request.agentID,
                    PolicyID = request.policyID,
                    CommissionAmount = request.commissionAmount,
                };
                await commissionRL.AddCommissionAsync(commissionMl);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }   
        }
    }
}
