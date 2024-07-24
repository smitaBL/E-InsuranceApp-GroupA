using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBL loginBL;
        private readonly ResponseML responseML;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginBL loginBL, ILogger<LoginController> logger)
        {
            this.loginBL = loginBL;
            responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginML model)
        {
            try
            {
                _logger.LogInformation($"Login attempt for user {model.Email}");

                var result = await loginBL.LoginAsync(model);

                responseML.Success = true;
                responseML.Message = $"Login Successful";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(LoginException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError(ex, "An error occurred while logging in the user {Email}.", model.Email);

                return StatusCode(404, responseML);
            }
        }


    }
}
