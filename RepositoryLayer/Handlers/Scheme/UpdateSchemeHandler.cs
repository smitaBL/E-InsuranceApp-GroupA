using MediatR;
using RepositoryLayer.Commands.Scheme;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Scheme
{
    public class UpdateSchemeHandler : IRequestHandler<UpdateSchemeCommand>
    {
        private readonly ISchemeRL schemeRL;

        public UpdateSchemeHandler(ISchemeRL schemeRL)
        {
            this.schemeRL = schemeRL;
        }

        public async Task<Unit> Handle(UpdateSchemeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SchemeEntity schemeEntity = new SchemeEntity()
                {
                    SchemeName = request.SchemeName,
                    SchemeDetails = request.SchemeDetails,  
                    SchemePrice = request.SchemePrice,
                    SchemeCover = request.SchemeCover,
                    SchemeTenure = request.SchemeTenure,
                    PlanID = request.PlanID,
                };

                await schemeRL.UpdateSchemeAsync(request.SchemeId, schemeEntity);

                return Unit.Value;
            }
            catch (SchemeException)
            {
                throw;
            }
        }
    }
}
