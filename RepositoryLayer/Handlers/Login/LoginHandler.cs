using MediatR;
using ModelLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly ILoginRL loginRL;

        public LoginHandler(ILoginRL loginRL)
        {
            this.loginRL = loginRL;
        }

        public Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var login = new LoginML()
            {
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            var result = loginRL.LoginAsync(login);

            return result;
        }
    }
}
