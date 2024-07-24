using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Agent;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Handlers.Agent
{
    public class CreateAgentHandler : IRequestHandler<CreateAgentCommand, InsuranceAgentEntity>
    {
        private readonly IAgentRL agentRL;

        public CreateAgentHandler(IAgentRL agentRL)
        {
            this.agentRL = agentRL;
        }

        public async Task<InsuranceAgentEntity> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            InsuranceAgentEntity insuranceAgent = new InsuranceAgentEntity
            {
                Username = request.username.ToLower(),
                FullName = request.fullName.ToLower(),
                Email = request.email.ToLower(),
                Password = PasswordHashing.Encrypt(request.password),
            };
            return await agentRL.CreateAgentAsync(insuranceAgent);
        }
    }
}
