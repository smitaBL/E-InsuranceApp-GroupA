using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.InsurancePlan
{
    public class CreateInsurancePlanCommand : IRequest
    {
        public string PlanName { get; set; }
        public string PlanDetails { get; set; }

        public CreateInsurancePlanCommand(string PlanName, string PlanDetails)
        {
            this.PlanName = PlanName;
            this.PlanDetails = PlanDetails;
        }
    }
}
