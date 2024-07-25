using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Commands.Admin;
using RepositoryLayer.Entity;
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

        public async Task<AdminEntity> RegisterAsync(AdminML model)
        {
            try
            {
                var result = await mediator.Send(new CreateAdminCommand(model.Username, model.Email, model.Password, model.FullName));
                return result;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}
