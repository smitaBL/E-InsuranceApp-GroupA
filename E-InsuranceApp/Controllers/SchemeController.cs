using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Tnef;
using ModelLayer;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]

    public class SchemeController : ControllerBase
    {
        private readonly ISchemeBL schemeBL;
        private readonly ResponseML responseML;

        public SchemeController(ISchemeBL schemeBL)
        {
            this.schemeBL = schemeBL;
            responseML = new ResponseML();
        }

        [HttpPost("CreateScheme")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> CreateSchemeAsync(SchemeML model)
        {
            try
            {
                int employeeID = Convert.ToInt32(User.FindFirst("Id").Value);
                Console.WriteLine(employeeID);
                await schemeBL.CreateSchemeAsync(model, employeeID);

                responseML.Success = true;
                responseML.Message = $"Scheme Name : {model.SchemeName} Created Successfully.";

                return StatusCode(201, responseML);
            }
            catch(SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpPut("UpdateSchemeById/{Id}")]
        public async Task<ActionResult> UpdateSchemeAsync(int Id, SchemeML model)
        {
            try
            {
                await schemeBL.UpdateSchemeAsync(Id,model);

                responseML.Success = true;
                responseML.Message = $"Scheme Name : {model.SchemeName} Updated Successfully.";

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpDelete("DeleteSchemeById/{Id}")]
        public async Task<ActionResult> DeleteSchemeAsync(int Id)
        {
            try
            {
                await schemeBL.DeleteSchemeAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Scheme ID : {Id} Deleted Successfully.";

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("GetAllScheme")]
        public async Task<ActionResult> GetAllSchemeAsync()
        {
            try
            {
                var result = await schemeBL.GetAllSchemeAsync();

                responseML.Success = true;
                responseML.Message = $"Scheme Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch(SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("GetSchemeById/{Id}")]
        public async Task<ActionResult> GetSchemeByIdAsync(int Id)
        {
            try
            {
                var result = await schemeBL.GetSchemeByIdAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Scheme ID : {Id} Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(404, responseML);
            }
        }
    }
}
