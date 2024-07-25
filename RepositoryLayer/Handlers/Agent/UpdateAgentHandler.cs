using BusinessLayer.Service;
using MediatR;
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
    public class UpdateAgentHandler:IRequestHandler<UpdateAgentCommand>
    {
        private readonly IAgentRL agentRL;

        public UpdateAgentHandler(IAgentRL agentRL)
        {
            this.agentRL = agentRL;
        }

        public async Task<Unit> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                InsuranceAgentEntity insuranceAgent = new InsuranceAgentEntity
                {
                    FullName = request.fullName.ToLower(),
                    Username = request.username.ToLower(),
                    Email = request.email.ToLower(),
                    Password = PasswordHashing.Encrypt(request.password),
                };
                await agentRL.UpdateAgentAsync(request.id,insuranceAgent);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }
    }
}
