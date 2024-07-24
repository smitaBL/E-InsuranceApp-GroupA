using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Admin
{
    public class DeleteAdminByIdCommand : IRequest
    {
        public int id;

        public DeleteAdminByIdCommand(int id)
        {
            this.id = id;
        }
    }
}
