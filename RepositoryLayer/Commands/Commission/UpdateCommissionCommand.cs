using MediatR;

namespace BusinessLayer.Service
{
    public class UpdateCommissionCommand : IRequest
    {
        public int agentID;
        public int policyID;
        public double commissionAmount;

        public UpdateCommissionCommand(int agentID, int policyID, double commissionAmount)
        {
            this.agentID = agentID;
            this.policyID = policyID;
            this.commissionAmount = commissionAmount;
        }
    }
}