using BusinessLayer.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Commands.InsurancePlan;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Queries.InsurancePlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class InsurancePlanBL : IInsurancePlanBL
    {
        private readonly IMediator mediator;

        public InsurancePlanBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task CreateInsurancePlan(InsurancePlanML model)
        {
            try
            {
                _ = await mediator.Send(new CreateInsurancePlanCommand(model.PlanName, model.PlanDetails));
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }

        public async Task DeleteInsurancePlanByIdAsync(int id)
        {
            try
            {
                await mediator.Send(new DeleteInsurancePlanCommand(id));
            }
            catch (InsurancePlanException)
            {
                throw;
            }

        }

        public async Task<List<InsurancePlanEntity>> GetAllInsurancePlanAsync()
        {
            try
            {
                return await mediator.Send(new GetAllInsurancePlanQuery());
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }

        public async Task<InsurancePlanEntity> GetInsurancePlanByIdAsync(int id)
        {
            try
            {
                return await mediator.Send(new GetInsurancePlanByIdQuery(id));
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }

        public async Task UpdateInsurancePlanByIdAsync(int id, InsurancePlanML model)
        {
            try
            {
                await mediator.Send(new UpdateInsurancePlanCommand(id, model.PlanName, model.PlanDetails));
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }
    }
}
