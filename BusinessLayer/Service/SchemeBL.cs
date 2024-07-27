using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.InsurancePlan;
using RepositoryLayer.Commands.Scheme;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Queries.Scheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class SchemeBL : ISchemeBL
    {
        private readonly IMediator mediator;

        public SchemeBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task CreateSchemeAsync(SchemeML model, int employeeId)
        {
            try
            {
                _ = await mediator.Send(new CreateSchemeCommand(model.SchemeName,
                                                                model.SchemeDetails,
                                                                model.SchemePrice,
                                                                model.SchemeCover,
                                                                model.SchemeTenure,
                                                                model.PlanID,
                                                                employeeId));
            }
            catch (SchemeException)
            {
                throw;
            }
        }

        public async Task DeleteSchemeAsync(int id)
        {
            try
            {
                _ = await mediator.Send(new DeleteSchemeCommand(id));
            }
            catch(SchemeException)
            {
                throw;
            }
        }

        public async Task UpdateSchemeAsync(int Id, SchemeML model)
        {
            try
            {
                await mediator.Send(new UpdateSchemeCommand(Id,
                                                            model.SchemeName,
                                                            model.SchemeDetails,
                                                            model.SchemePrice,
                                                            model.SchemeCover,
                                                            model.SchemeTenure,
                                                            model.PlanID));
            }
            catch(SchemeException)
            {
                throw;
            }
        }

        public async Task<List<SchemeWithInsurancePlanML>> GetAllSchemeAsync()
        {
            try
            {
                return await mediator.Send(new GetAllSchemeQuery());
            }
            catch (SchemeException)
            {
                throw;
            }
        }

        public async Task<SchemeWithInsurancePlanML> GetSchemeByIdAsync(int id)
        {
            try
            {
                return await mediator.Send(new GetSchemeByIdQuery(id));
            }
            catch (SchemeException)
            {
                throw;
            }
        }
    }
}
