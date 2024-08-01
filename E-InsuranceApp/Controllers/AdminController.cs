using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        private readonly ResponseML responseML;
        private readonly ILogger<LoginController> _logger;

        public AdminController(IAdminBL adminBL, ILogger<LoginController> logger)
        {
            this.adminBL = adminBL;
            responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegisterAdminAsync(AdminML model)
        {
            try
            {
                _logger.LogInformation($"Register attempt for user {model.Email}");
                await adminBL.RegisterAsync(model);
                
                    responseML.Success = true;
                    responseML.Message = "Admin Created Successfully";
            }
            catch (AdminException ex)
            {
                _logger.LogError(ex, "An error occurred while registering in the user {Email}.", model.Email);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(201, responseML);
        }

        [HttpGet("Admin/GetALL")]
        public async Task<IActionResult> GetAllAdminAsync()
        {
            try
            {
                _logger.LogInformation("Fetching attempt for all admins");
                var result = await adminBL.GetAllAdminAsync();
                if (result != null)
                {

                    responseML.Success = true;
                    responseML.Message = "Admin Fetched Successfully";
                    responseML.Data = result;

                }
            }
            catch (AdminException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all admins.");
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpGet("Admin/GetById")]
        public async Task<IActionResult> GetAdminByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching attempt for admin with id {id}");
                var result = await adminBL.GetAdminByIdAsync(id);
                if (result != null)
                {

                    responseML.Success = true;
                    responseML.Message = "Admin Fetched Successfully";
                    responseML.Data = result;

                }
            }
            catch (AdminException ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching admin with id {id}");
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpDelete("Admin/DeleteById")]
        public async Task<IActionResult> DeleteAdminByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Delete attempt for admin with id {id}");
                await adminBL.DeleteAdminByIdAsync(id);

                    responseML.Success = true;
                    responseML.Message = "Admin Deleted Successfully";
           
            }
            catch (AdminException ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting admin with id {id}");
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpPut("Admin/UpdateById")]
        public async Task<IActionResult> UpdateAdminByIdAsync(int id, AdminML model)
        {
            try
            {
                _logger.LogInformation($"Update attempt for admin with id {id}");
                await adminBL.UpdateAdminByIdAsync(id,model);

                responseML.Success = true;
                responseML.Message = "Admin Updated Successfully";

            }
            catch (AdminException ex)
            {
                _logger.LogError(ex, $"An error occurred while updating admin with id {id}");
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

    }
}
