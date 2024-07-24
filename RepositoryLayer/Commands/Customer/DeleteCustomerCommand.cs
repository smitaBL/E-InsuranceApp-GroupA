using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Customer
{
    public class DeleteCustomerCommand : IRequest
    {
        public int id;

        public DeleteCustomerCommand(int id)
        {
            this.id = id;
        }
    }
}
