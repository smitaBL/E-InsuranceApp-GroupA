using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GetAllAgentCommand : IRequest<List<InsuranceAgentEntity>>
    {
    }
}