using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Admin
{
    public class CreateAdminCommand : IRequest
    {
        public string username;
        public string email;
        public string password;
        public string fullName;

        public CreateAdminCommand(string username, string email, string password, string fullName)
        {
            this.username = username;
            this.email = email;
            this.password = password;
            this.fullName = fullName;
        }
    }
}
