using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class CreateEmployeeCommand : IRequest<EmployeeEntity>
    {
        public string username;
        public string password;
        public string fullName;
        public string email;
        public string role;

        public CreateEmployeeCommand(string username, string password, string fullName, string email, string role)
        {
            this.username = username;
            this.password = password;
            this.fullName = fullName;
            this.email = email;
            this.role = role;
        }
    }
}