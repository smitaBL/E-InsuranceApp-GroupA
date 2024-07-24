using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Customer
{
    public class CreateCustomerCommand : IRequest<CustomerEntity>
    {
        public string username;
        public string fullName;
        public string email;
        public string password;
        public string phone;
        public DateTime dateOfBirth;
        public int agentID;

        public CreateCustomerCommand(string username, string fullName, string email, string password, string phone, DateTime dateOfBirth, int agentID)
        {
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
