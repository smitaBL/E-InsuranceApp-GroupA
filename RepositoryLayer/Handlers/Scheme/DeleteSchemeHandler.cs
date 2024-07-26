using MediatR;
using RepositoryLayer.Commands.Scheme;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Scheme
{
    public class DeleteSchemeHandler : IRequestHandler<DeleteSchemeCommand>
    {
        private readonly ISchemeRL schemeRL;

        public DeleteSchemeHandler(ISchemeRL schemeRL)
        {
            this.schemeRL = schemeRL;
        }

        public async Task<Unit> Handle(DeleteSchemeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await schemeRL.DeleteSchemeAsync(request.Id);

                return Unit.Value;
            }
            catch (SchemeException)
            {
                throw;
            }
        }
    }
}
