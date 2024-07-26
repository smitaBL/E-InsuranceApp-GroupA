using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Agent;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CommissionBL : ICommissionBL
    {
        private readonly IMediator mediator;
        public CommissionBL(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task AddCommissionAsync(CommissionML commissionML)
        {
            try
            {
                await mediator.Send(new AddCommissionCommand(commissionML.AgentID,commissionML.PolicyID,commissionML.CommissionAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCommissionAsync(int agentId, int policyId)
        {
            try
            {
                await mediator.Send(new DeleteCommissionCommand(agentId,policyId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CommissionEntity>> GetAllCommissionAsync()
        {
            try
            {
                return await mediator.Send(new GetAllCommissionCommand());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CommissionEntity> GetByIdCommissionAsync(int agentId, int policyId)
        {
            try
            {
                return await mediator.Send(new GetByIdCommissionCommand(agentId, policyId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateCommissionAsync(CommissionML commissionML)
        {
            try
            {
                await mediator.Send(new UpdateCommissionCommand(commissionML.AgentID,commissionML.PolicyID,commissionML.CommissionAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
