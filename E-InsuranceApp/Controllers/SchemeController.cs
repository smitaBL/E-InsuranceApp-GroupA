using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeBL schemeBL;
        private readonly ResponseML responseML;
        private readonly ILogger<SchemeController> _logger;

        public SchemeController(ISchemeBL schemeBL, ILogger<SchemeController> logger)
        {
            this.schemeBL = schemeBL;
            responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("CreateScheme")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> CreateSchemeAsync(SchemeML model)
        {
            try
            {
                int employeeID = Convert.ToInt32(User.FindFirst("Id").Value);
                _logger.LogInformation($"Employee with ID {employeeID} is creating a new scheme.");
                await schemeBL.CreateSchemeAsync(model, employeeID);

                responseML.Success = true;
                responseML.Message = $"Scheme Name: {model.SchemeName} Created Successfully.";
                _logger.LogInformation($"Scheme {model.SchemeName} created successfully by employee ID {employeeID}.");

                return StatusCode(201, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                _logger.LogError($"Error creating scheme: {ex.Message}");

                return StatusCode(500, responseML);
            }
        }

        [HttpPut("UpdateSchemeById/{Id}")]
        public async Task<ActionResult> UpdateSchemeAsync(int Id, SchemeML model)
        {
            try
            {
                _logger.LogInformation($"Updating scheme with ID {Id}.");
                await schemeBL.UpdateSchemeAsync(Id, model);

                responseML.Success = true;
                responseML.Message = $"Scheme Name: {model.SchemeName} Updated Successfully.";
                _logger.LogInformation($"Scheme ID {Id} updated successfully.");

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                _logger.LogError($"Error updating scheme with ID {Id}: {ex.Message}");

                return StatusCode(404, responseML);
            }
        }

        [HttpDelete("DeleteSchemeById/{Id}")]
        public async Task<ActionResult> DeleteSchemeAsync(int Id)
        {
            try
            {
                _logger.LogInformation($"Deleting scheme with ID {Id}.");
                await schemeBL.DeleteSchemeAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Scheme ID: {Id} Deleted Successfully.";
                _logger.LogInformation($"Scheme ID {Id} deleted successfully.");

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                _logger.LogError($"Error deleting scheme with ID {Id}: {ex.Message}");

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("GetAllScheme")]
        public async Task<ActionResult> GetAllSchemeAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all schemes.");
                var result = await schemeBL.GetAllSchemeAsync();

                responseML.Success = true;
                responseML.Message = "Schemes Fetched Successfully.";
                responseML.Data = result;
                _logger.LogInformation("All schemes fetched successfully.");

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                _logger.LogError($"Error fetching all schemes: {ex.Message}");

                return StatusCode(404, responseML);
            }
        }

        [HttpGet("GetSchemeById/{Id}")]
        public async Task<ActionResult> GetSchemeByIdAsync(int Id)
        {
            try
            {
                _logger.LogInformation($"Fetching scheme with ID {Id}.");
                var result = await schemeBL.GetSchemeByIdAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Scheme ID: {Id} Fetched Successfully.";
                responseML.Data = result;
                _logger.LogInformation($"Scheme ID {Id} fetched successfully.");

                return StatusCode(200, responseML);
            }
            catch (SchemeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                _logger.LogError($"Error fetching scheme with ID {Id}: {ex.Message}");

                return StatusCode(404, responseML);
            }
        }
    }
}
