using MediatR;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Admin
{
    public class UpdateAdminHandler : IRequestHandler<UpdateAdminCommand>
    {
        private readonly IAdminRL adminRL;

        public UpdateAdminHandler(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AdminEntity admin = new AdminEntity()
                {
                    Username = request.username.ToLower(),
                    Password = PasswordHashing.Encrypt(request.password),
                    Email = request.email.ToLower(),
                    FullName = request.fullName.ToLower()
                };
                await adminRL.UpdateAdminAsync(request.id,admin);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }

        }
    }
}
