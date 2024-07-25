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
    public class GetInsurancePlanByIdHandler : IRequestHandler<GetInsurancePlanByIdQuery, InsurancePlanEntity>
    {
        private readonly IInsurancePlanRL insurancePlanRL;

        public GetInsurancePlanByIdHandler(IInsurancePlanRL insurancePlanRL)
        {
            this.insurancePlanRL = insurancePlanRL;
        }

        public async Task<InsurancePlanEntity> Handle(GetInsurancePlanByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await insurancePlanRL.GetInsurancePlanByIdAsync(request.Id);
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }
    }
}
