using MediatR;

namespace BusinessLayer.Service
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int id;

        public DeleteEmployeeCommand(int id)
        {
            this.id = id;
        }
    }
}