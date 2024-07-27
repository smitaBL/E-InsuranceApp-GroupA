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
    public class CreateSchemeHandler : IRequestHandler<CreateSchemeCommand>
    {
        private readonly ISchemeRL schemeRL;

        public CreateSchemeHandler(ISchemeRL schemeRL)
        {
            this.schemeRL = schemeRL;
        }

        public async Task<Unit> Handle(CreateSchemeCommand request, CancellationToken cancellationToken)
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
                    PlanID = request.PlanID
                };

                await schemeRL.CreateSchemeAsync(schemeEntity, request.EmployeeId);

                return Unit.Value;
            }
            catch (SchemeException)
            {
                throw;
            }

        }
    }
}
