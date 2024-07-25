using MediatR;
using RepositoryLayer.Commands.InsurancePlan;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.InsurancePlan
{
    public class UpdateInsurancePlanHandler : IRequestHandler<UpdateInsurancePlanCommand>
    {
        private readonly IInsurancePlanRL insurancePlanRL;

        public UpdateInsurancePlanHandler(IInsurancePlanRL insurancePlanRL)
        {
            this.insurancePlanRL = insurancePlanRL;
        }

        public async Task<Unit> Handle(UpdateInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            InsurancePlanEntity insurancePlanEntity = new() 
            {
                PlanName = request.PlanName,
                PlanDetails = request.PlanDetails
            };

            await insurancePlanRL.UpdateInsurancePlanByIdAsync(request.Id, insurancePlanEntity);

            return Unit.Value;
        }
    }
}
