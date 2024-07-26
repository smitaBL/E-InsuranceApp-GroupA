using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Agent;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Agent
{
    public class CreateAgentHandler : IRequestHandler<CreateAgentCommand>
    {
        private readonly IAgentRL agentRL;

        public CreateAgentHandler(IAgentRL agentRL)
        {
            this.agentRL = agentRL;
        }

        public async Task<Unit> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                InsuranceAgentEntity insuranceAgent = new InsuranceAgentEntity
                {
                    Username = request.username.ToLower(),
                    FullName = request.fullName.ToLower(),
                    Email = request.email.ToLower(),
                    Password = PasswordHashing.Encrypt(request.password),
                };
                await agentRL.CreateAgentAsync(insuranceAgent);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }
    }
}
