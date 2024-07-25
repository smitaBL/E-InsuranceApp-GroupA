using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Customer
{
    public class UpdateCustomerCommand : IRequest
    {
        public int id;
        public string username;
        public string fullName;
        public string email;
        public string password;
        public string phone;
        public DateTime dateOfBirth;
        public int agentID;

        public UpdateCustomerCommand(int id, string username, string fullName, string email, string password, string phone, DateTime dateOfBirth, int agentID)
        {
            this.id = id;
            this.username = username;
            this.fullName = fullName;
            this.email = email;
            this.password = password;
            this.phone = phone;
            this.dateOfBirth = dateOfBirth;
            this.agentID = agentID;
        }
    }
}
