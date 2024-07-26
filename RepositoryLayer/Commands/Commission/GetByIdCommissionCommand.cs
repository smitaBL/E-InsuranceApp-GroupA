using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GetByIdCommissionCommand : IRequest<CommissionEntity>
    {
        public int agentId;
        public int policyId;

        public GetByIdCommissionCommand(int agentId, int policyId)
        {
            this.agentId = agentId;
            this.policyId = policyId;
        }
    }
}