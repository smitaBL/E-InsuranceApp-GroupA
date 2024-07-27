using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Scheme
{
    public class DeleteSchemeCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteSchemeCommand(int id)
        {
            Id = id;
        }
    }
}
