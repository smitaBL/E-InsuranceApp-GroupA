using BusinessLayer.Service;
using MediatR;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Agent
{
    public class GetByIdAgentHandler:IRequestHandler<GetByIdAgentCommand,InsuranceAgentEntity>
    {
        private readonly IAgentRL agentRL;

        public GetByIdAgentHandler(IAgentRL agentRL)
        {
            this.agentRL = agentRL;
        }

        public async Task<InsuranceAgentEntity> Handle(GetByIdAgentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await agentRL.GetByIdAgentAsync(request.id);
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }
    }
}
