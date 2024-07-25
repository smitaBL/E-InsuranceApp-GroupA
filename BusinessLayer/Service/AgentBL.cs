using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Agent;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class AgentBL : IAgentBL
    {
        private readonly IMediator mediator;
        public AgentBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task CreateAgentAsync(InsuranceAgentML insuranceAgentML)
        {
            try
            {
                await mediator.Send(new CreateAgentCommand( insuranceAgentML.Username, insuranceAgentML.FullName, insuranceAgentML.Email,insuranceAgentML.Password));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAgentAsync(int id)
        {
            try
            {
                await mediator.Send(new DeleteAgentCommand(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<InsuranceAgentEntity>> GetAllAgentAsync()
        {
            try
            {
                return await mediator.Send(new GetAllAgentCommand());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InsuranceAgentEntity> GetByIdAgentAsync(int id)
        {
            try
            {
                return await mediator.Send(new GetByIdAgentCommand(id));
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task UpdateAgentAsync(int id, InsuranceAgentML insuranceAgentML)
        {
            try
            {
                await mediator.Send(new UpdateAgentCommand(id,insuranceAgentML.FullName,insuranceAgentML.Username,insuranceAgentML.Email,insuranceAgentML.Password));
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
