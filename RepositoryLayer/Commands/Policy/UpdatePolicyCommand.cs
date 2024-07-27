using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Policy
{
    public class UpdatePolicyCommand : IRequest
    {
        public int id;
        public int customerID;
        public int schemeID;
        public string policyDetails;
        public double premium;
        public DateTime dateIssued;
        public int maturityPeriod;
        public DateTime policyLapseDate;

        public UpdatePolicyCommand(int id, int customerID, int schemeID, string policyDetails, double premium, DateTime dateIssued, int maturityPeriod, DateTime policyLapseDate)
        {
            this.id = id;
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
