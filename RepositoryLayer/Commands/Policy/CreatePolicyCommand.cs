using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Policy
{
    public class CreatePolicyCommand : IRequest
    {
        public int customerID;
        public int schemeID;
        public string policyDetails;
        public double premium;
        public DateTime dateIssued;
        public int maturityPeriod;
        public DateTime policyLapseDate;

        public CreatePolicyCommand(int customerID, int schemeID, string policyDetails, double premium, DateTime dateIssued, int maturityPeriod, DateTime policyLapseDate)
        {
            this.customerID = customerID;
            this.schemeID = schemeID;
            this.policyDetails = policyDetails;
            this.premium = premium;
            this.dateIssued = dateIssued;
            this.maturityPeriod = maturityPeriod;
            this.policyLapseDate = policyLapseDate;
        }
    }
}
