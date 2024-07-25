using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Admin
{
    public class UpdateAdminCommand : IRequest
    {
        public int id;
        public string username;
        public string email;
        public string password;
        public string fullName;

        public UpdateAdminCommand(int id, string username, string email, string password, string fullName)
        {
            this.id = id;
            this.username = username;
            this.email = email;
            this.password = password;
            this.fullName = fullName;
        }
    }
}
