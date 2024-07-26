using BusinessLayer.Service;
using MediatR;
using ModelLayer;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Commission
{
    public class DeleteCommissionHandler:IRequestHandler<DeleteCommissionCommand>
    {
        private readonly ICommissionRL commissionRL;

        public DeleteCommissionHandler(ICommissionRL commissionRL)
        {
            this.commissionRL = commissionRL;
        }

        public async Task<Unit> Handle(DeleteCommissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await commissionRL.DeleteCommissionAsync(request.agentId,request.policyId);
                return Unit.Value;
            }
            catch (Exception ex) 
            {
                throw new CommissionException(ex.Message);
            }
        }
    }
}
