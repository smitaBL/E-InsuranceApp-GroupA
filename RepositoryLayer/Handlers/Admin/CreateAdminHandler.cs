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
    public class CreateAdminHandler : IRequestHandler<CreateAdminCommand>
    {
        private readonly IAdminRL adminRL;

        public CreateAdminHandler(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
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
                await adminRL.RegisterAsync(admin);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
            
        }

        
    }
}
