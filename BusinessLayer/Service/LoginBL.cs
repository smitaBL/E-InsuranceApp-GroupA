using BusinessLayer.Interface;
using MediatR;
using ModelLayer;
using RepositoryLayer.Queries.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LoginBL : ILoginBL
    {
        private readonly IMediator mediator;

        public LoginBL(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<string> LoginAsync(LoginML model)
        {
            try
            {
                var result = await mediator.Send(new LoginQuery(model.Email, model.Password, model.Role));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
