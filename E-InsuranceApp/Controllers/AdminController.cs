using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        private readonly ResponseML responseML;

        public AdminController(IAdminBL adminBL, ResponseML responseML)
        {
            this.adminBL = adminBL;
            responseML = new ResponseML();
        }



        [AllowAnonymous]
        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegisterAdminAsync(AdminML model)
        {
            try
            {
                var result = await adminBL.RegisterAsync(model);
                if (result != null)
                {

                    responseML.Success = true;
                    responseML.Message = "Admin Created Successfully";
                    responseML.Data = result;
                    
                }
            }
            catch (AdminException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(201, responseML);
        }
    }
}
