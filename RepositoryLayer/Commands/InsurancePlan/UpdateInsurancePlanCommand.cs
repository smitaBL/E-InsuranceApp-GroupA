using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.InsurancePlan
{
    public class UpdateInsurancePlanCommand : IRequest
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string PlanDetails { get; set; }

        public UpdateInsurancePlanCommand(int Id, string PlanName, string PlanDetails)
        {
            this.Id = Id;
            this.PlanName = PlanName;
            this.PlanDetails = PlanDetails;
        }
    }
}
