using MediatR;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Admin
{
    public class DeleteAdminByIdHandler : IRequestHandler<DeleteAdminByIdCommand>
    {
        private readonly IAdminRL adminRL;

        public DeleteAdminByIdHandler(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<Unit> Handle(DeleteAdminByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await adminRL.DeleteAdminByIdAsync(request.id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
