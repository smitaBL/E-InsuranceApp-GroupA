using MediatR;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Admin
{
    public class CreateAdminHandler : IRequestHandler<CreateAdminCommand, AdminEntity>
    {
        private readonly IAdminRL adminRL;

        public CreateAdminHandler(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<AdminEntity> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            AdminEntity admin = new AdminEntity()
            {
                Username = request.username.ToLower(),
                Password = PasswordHashing.Encrypt(request.password),
                Email = request.email.ToLower(),
                FullName = request.fullName.ToLower()
            };
            var result = await adminRL.RegisterAsync(admin);
            return result;
        }
    }
}
