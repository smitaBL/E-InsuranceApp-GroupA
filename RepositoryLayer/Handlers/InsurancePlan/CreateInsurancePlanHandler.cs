using MediatR;
using RepositoryLayer.Commands.InsurancePlan;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.InsurancePlan
{
    public class CreateInsurancePlanHandler : IRequestHandler<CreateInsurancePlanCommand>
    {
        private readonly IInsurancePlanRL insurancePlanRL;

        public CreateInsurancePlanHandler(IInsurancePlanRL insurancePlanRL)
        {
            this.insurancePlanRL = insurancePlanRL;
        }

        public async Task<Unit> Handle(CreateInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                InsurancePlanEntity insurancePlanEntity = new InsurancePlanEntity()
                {
                    PlanName = request.PlanName,
                    PlanDetails = request.PlanDetails,
                };

                await insurancePlanRL.CreateInsurancePlan(insurancePlanEntity);
                return Unit.Value;
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }
    }
}
