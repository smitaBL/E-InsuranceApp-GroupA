using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Agent
{
    public class CreateAgentCommand : IRequest<InsuranceAgentEntity>
    {
        public string username;
        public string fullName;
        public string email;
        public string password;

        public CreateAgentCommand(string username, string fullName, string email, string password)
        {
            this.username = username;
            this.fullName = fullName;
            this.email = email;
            this.password = password;
        }
    }
}
