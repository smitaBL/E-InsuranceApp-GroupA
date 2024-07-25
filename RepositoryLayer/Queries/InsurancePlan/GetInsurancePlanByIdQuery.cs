using MediatR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.InsurancePlan
{
    public class GetInsurancePlanByIdQuery : IRequest<InsurancePlanEntity>
    {
        public int Id { get; set; }

        public GetInsurancePlanByIdQuery(int id)
        {
            Id = id;
        }
    }
}
