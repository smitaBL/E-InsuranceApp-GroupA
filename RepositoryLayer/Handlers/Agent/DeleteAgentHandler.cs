using BusinessLayer.Service;
using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Agent
{
    public class DeleteAgentHandler : IRequestHandler<DeleteAgentCommand>
    {
        private readonly IAgentRL agentRL;

        public DeleteAgentHandler(IAgentRL agentRL)
        {
            this.agentRL = agentRL;
        }

       
        public async Task<Unit> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await agentRL.DeleteAgentAsync(request.id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }
    }
}
