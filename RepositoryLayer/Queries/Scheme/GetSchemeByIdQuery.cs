using MediatR;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Scheme
{
    public class GetSchemeByIdQuery : IRequest<SchemeWithInsurancePlanML>
    {
        public int Id { get; set; }

        public GetSchemeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
