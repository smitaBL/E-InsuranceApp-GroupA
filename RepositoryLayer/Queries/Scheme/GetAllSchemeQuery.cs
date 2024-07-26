using MediatR;
using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Scheme
{
    public class GetAllSchemeQuery : IRequest<List<SchemeWithInsurancePlanML>>
    {
    }
}
