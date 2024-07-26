using MediatR;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GetByIdEmployeeCommand : IRequest<EmployeeEntity>
    {
        public int id;

        public GetByIdEmployeeCommand(int id)
        {
            this.id = id;
        }
    }
}