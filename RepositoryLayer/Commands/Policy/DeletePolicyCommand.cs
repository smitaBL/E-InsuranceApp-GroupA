using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Policy
{
    public class DeletePolicyCommand : IRequest
    {
        public int id;

        public DeletePolicyCommand(int id)
        {
            this.id = id;
        }
    }
}
