using MediatR;
using ModelLayer;

namespace BusinessLayer.Service
{
    public class UpdateAgentCommand : IRequest
    {
        public int id;
        public string fullName;
        public string username;
        public string email;
        public string password;

        public UpdateAgentCommand(int id, string fullName, string username, string email, string password)
        {
            this.id = id;
            this.fullName = fullName;
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }
}