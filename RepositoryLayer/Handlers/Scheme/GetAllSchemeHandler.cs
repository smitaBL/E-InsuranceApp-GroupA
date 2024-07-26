using MediatR;
using ModelLayer;
using RepositoryLayer.Entity;
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
    public class GetAllSchemeHandler : IRequestHandler<GetAllSchemeQuery, List<SchemeWithInsurancePlanML>>
    {
        private readonly ISchemeRL schemeRL;

        public GetAllSchemeHandler(ISchemeRL schemeRL)
        {
            this.schemeRL = schemeRL;
        }

        public async Task<List<SchemeWithInsurancePlanML>> Handle(GetAllSchemeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await schemeRL.GetAllSchemeAsync();
            }
            catch (SchemeException)
            {
                throw;
            }
        }
    }
}
