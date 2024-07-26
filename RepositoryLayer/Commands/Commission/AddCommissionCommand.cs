using MediatR;

namespace BusinessLayer.Service
{
    public class AddCommissionCommand : IRequest
    {
        public int agentID;
        public int policyID;
        public double commissionAmount;

        public AddCommissionCommand(int agentID, int policyID, double commissionAmount)
        {
            this.agentID = agentID;
            this.policyID = policyID;
            this.commissionAmount = commissionAmount;
        }
    }
}