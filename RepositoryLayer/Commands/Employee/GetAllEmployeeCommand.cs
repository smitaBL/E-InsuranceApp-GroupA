using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GetAllEmployeeCommand : IRequest<List<EmployeeEntity>>
    {
    }
}