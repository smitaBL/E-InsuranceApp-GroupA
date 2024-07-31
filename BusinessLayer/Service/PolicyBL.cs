using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Commands.Policy;
using RepositoryLayer.Entity;
using RepositoryLayer.Queries.Policy;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class PolicyBL : IPolicyBL
    {
        private readonly IMediator mediator;

        public PolicyBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task AddPolicyAsync(int customerid, PolicyML model)
        {
            try
            {
                await mediator.Send(new CreatePolicyCommand(customerid, model.SchemeID,model.PolicyDetails,model.Premium,model.DateIssued,model.MaturityPeriod,model.PolicyLapseDate));
            }
            catch
            {
                throw;
            }
        }

        public async Task DeletePolicyByIdAsync(int id)
        {
            try
            {
                await mediator.Send(new DeletePolicyCommand(id));
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PolicyDTO>> GetAllPoliciesAsync(int customerid)
        {
            try
            {
                return await mediator.Send(new GetAllPoliciesQuery(customerid));
            }
            catch
            {
                throw;
            }
        }

        public async Task<PolicyDTO> GetPolicyByIdAsync(int policyid)
        {
            try
            {
                return await mediator.Send(new GetPolicyByIdQuery(policyid));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdatePolicyByIdAsync(int id,int customerid, PolicyML model)
        {
            try
            {
                await mediator.Send(new UpdatePolicyCommand(id, customerid, model.SchemeID, model.PolicyDetails, model.Premium, model.DateIssued, model.MaturityPeriod, model.PolicyLapseDate));
            }
            catch
            {
                throw;
            }
        }
    }
}
