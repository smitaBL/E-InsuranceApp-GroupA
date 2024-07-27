using MediatR;
using ModelLayer;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Scheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Scheme
{
    internal class GetSchemeByIdHandler : IRequestHandler<GetSchemeByIdQuery, SchemeWithInsurancePlanML>
    {
        private readonly ISchemeRL schemeRL;

        public GetSchemeByIdHandler(ISchemeRL schemeRL)
        {
            this.schemeRL = schemeRL;
        }

        public Task<SchemeWithInsurancePlanML> Handle(GetSchemeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return schemeRL.GetSchemeByIdAsync(request.Id);
            }
            catch (SchemeException)
            {
                throw;
            }
        }
    }
}
