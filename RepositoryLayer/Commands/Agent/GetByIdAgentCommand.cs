using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GetByIdAgentCommand : IRequest<InsuranceAgentEntity>
    {
        public int id;

        public GetByIdAgentCommand(int id)
        {
            this.id = id;
        }
    }
}