using MediatR;

namespace BusinessLayer.Service
{
    public class DeleteAgentCommand : IRequest
    {
        public int id;

        public DeleteAgentCommand(int id)
        {
            this.id = id;
        }
    }
}