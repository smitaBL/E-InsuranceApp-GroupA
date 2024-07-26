using BusinessLayer.Service;
using MediatR;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
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
    public class GetAllCommissionHandler : IRequestHandler<GetAllCommissionCommand, List<CommissionEntity>>
    {
        private readonly ICommissionRL commissionRL;

        public GetAllCommissionHandler(ICommissionRL commissionRL)
        {
            this.commissionRL = commissionRL;
        }

        public async Task<List<CommissionEntity>> Handle(GetAllCommissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await commissionRL.GetAllCommissionAsync();
            }
            catch (Exception ex)
            {
                throw new CommissionException(ex.Message);
            }
        }
    }
}
