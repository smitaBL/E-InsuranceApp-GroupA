using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GetAllCommissionCommand : IRequest<List<CommissionEntity>>
    {
    }
}