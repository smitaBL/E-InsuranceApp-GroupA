using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Admin
{
    public class GetAdminByIdHandler : IRequestHandler<GetAdminByIdQuery, AdminEntity>
    {
        private readonly IAdminRL adminRL;

        public GetAdminByIdHandler(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<AdminEntity> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await adminRL.GetAdminByIdAsync(request.id);
                return result;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
