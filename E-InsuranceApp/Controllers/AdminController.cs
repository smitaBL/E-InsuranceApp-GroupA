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

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
            responseML = new ResponseML();
        }

        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegisterAdminAsync(AdminML model)
        {
            try
            {
                await adminBL.RegisterAsync(model);
                
                    responseML.Success = true;
                    responseML.Message = "Admin Created Successfully";
            }
            catch (AdminException ex)
            {
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
                await adminBL.DeleteAdminByIdAsync(id);

                    responseML.Success = true;
                    responseML.Message = "Admin Deleted Successfully";
           
            }
            catch (AdminException ex)
            {
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
                await adminBL.UpdateAdminByIdAsync(id,model);

                responseML.Success = true;
                responseML.Message = "Admin Deleted Successfully";

            }
            catch (AdminException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }
    }
}
