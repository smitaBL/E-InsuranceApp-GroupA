using MediatR;

namespace BusinessLayer.Service
{
    public class UpdateCommissionCommand : IRequest
    {
        public int agentID;
        public int policyID;
        public float commissionPercentage;

        //public double commissionAmount;

        public UpdateCommissionCommand(int agentID, int policyID, float commissionPercentage)
        {
            this.agentID = agentID;
            this.policyID = policyID;
            this.commissionPercentage = commissionPercentage;
            //this.commissionAmount = commissionAmount;
        }
    }
}