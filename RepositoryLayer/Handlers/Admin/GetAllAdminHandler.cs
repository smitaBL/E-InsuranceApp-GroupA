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
    public class GetAllAdminHandler : IRequestHandler<GetAllAdminQuery,List<AdminEntity>>
    {
        private readonly IAdminRL adminRL;

        public GetAllAdminHandler(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<List<AdminEntity>> Handle(GetAllAdminQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await adminRL.GetAllAdminAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
