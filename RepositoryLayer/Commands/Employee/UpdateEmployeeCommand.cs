using MediatR;
using ModelLayer;

namespace BusinessLayer.Service
{
    public class UpdateEmployeeCommand : IRequest
    {
        public int id;
        public string username;
        public string password;
        public string email;
        public string fullName;
        public string role;

        public UpdateEmployeeCommand(int id, string username, string password, string email, string fullName, string role)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.email = email;
            this.fullName = fullName;
            this.role = role;
        }
    }
}