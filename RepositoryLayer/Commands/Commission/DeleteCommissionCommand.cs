using MediatR;

namespace BusinessLayer.Service
{
    public class DeleteCommissionCommand : IRequest
    {
        public int agentId;
        public int policyId;

        public DeleteCommissionCommand(int agentId, int policyId)
        {
            this.agentId = agentId;
            this.policyId = policyId;
        }
    }
}