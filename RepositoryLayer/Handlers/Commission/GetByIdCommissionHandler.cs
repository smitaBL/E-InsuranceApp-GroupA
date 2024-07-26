using BusinessLayer.Service;
using MediatR;
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
    public class GetByIdCommissionHandler : IRequestHandler<GetByIdCommissionCommand, CommissionEntity>
    {
        private readonly ICommissionRL commissionRL;

        public GetByIdCommissionHandler(ICommissionRL commissionRL)
        {
            this.commissionRL = commissionRL;
        }

        public async Task<CommissionEntity> Handle(GetByIdCommissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await commissionRL.GetByIdCommissionAsync(request.agentId, request.policyId);
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }
    }
}
