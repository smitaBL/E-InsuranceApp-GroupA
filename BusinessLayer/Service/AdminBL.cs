using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Entity;
using RepositoryLayer.Queries.Admin;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class AdminBL : IAdminBL
    {
        private readonly IMediator mediator;

        public AdminBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task DeleteAdminByIdAsync(int id)
        {
            try
            {
                await mediator.Send(new DeleteAdminByIdCommand(id));
            }
            catch
            {
                throw;
            }
        }

        public async Task<AdminEntity> GetAdminByIdAsync(int id)
        {
            try
            {
                var result = await mediator.Send(new GetAdminByIdQuery(id));
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<AdminEntity>> GetAllAdminAsync()
        {
            try
            {
                var result = await mediator.Send(new GetAllAdminQuery());
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task RegisterAsync(AdminML model)
        {
            try
            {
                await mediator.Send(new CreateAdminCommand(model.Username, model.Email, model.Password, model.FullName));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAdminByIdAsync(int id, AdminML model)
        {
            try
            {
                await mediator.Send(new UpdateAdminCommand(id,model.Username, model.Email, model.Password, model.FullName));
            }
            catch
            {
                throw;
            }
        }
    }
}
