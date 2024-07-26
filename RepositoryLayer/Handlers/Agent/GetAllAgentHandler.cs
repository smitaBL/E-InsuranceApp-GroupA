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
    public class GetAllAgentHandler:IRequestHandler<GetAllAgentCommand,List<InsuranceAgentEntity>>
    {
        private readonly IAgentRL agentRL;

        public GetAllAgentHandler(IAgentRL agentRL)
        {
            this.agentRL = agentRL;
        }

        public async Task<List<InsuranceAgentEntity>> Handle(GetAllAgentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await agentRL.GetAllAgentAsync();
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }
    }
}
