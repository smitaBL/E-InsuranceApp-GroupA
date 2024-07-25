using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.InsurancePlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.InsurancePlan
{
    public class GetAllInsurancePlanHandler : IRequestHandler<GetAllInsurancePlanQuery, List<InsurancePlanEntity>>
    {
        private readonly IInsurancePlanRL insurancePlanRL;

        public GetAllInsurancePlanHandler(IInsurancePlanRL insurancePlanRL)
        {
            this.insurancePlanRL = insurancePlanRL;
        }

        public async Task<List<InsurancePlanEntity>> Handle(GetAllInsurancePlanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await insurancePlanRL.GetAllInsurancePlanAsync();
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }
    }
}
