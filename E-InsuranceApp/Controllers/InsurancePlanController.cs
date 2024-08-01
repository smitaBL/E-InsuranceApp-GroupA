using BusinessLayer.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;
using Microsoft.Extensions.Logging;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanBL insurancePlanBL;
        private readonly ResponseML responseML;
        private readonly ILogger<InsurancePlanController> _logger;

        public InsurancePlanController(IInsurancePlanBL insurancePlanBL, ILogger<InsurancePlanController> logger)
        {
            this.insurancePlanBL = insurancePlanBL;
            responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("CreateInsurancePlan")]
        public async Task<ActionResult> CreateInsurancePlan(InsurancePlanML model)
        {
            try
            {
                _logger.LogInformation($"Attempting to create a new insurance plan {model.PlanName}");
                await insurancePlanBL.CreateInsurancePlan(model);

                responseML.Success = true;
                responseML.Message = "Insurance Plan Added Successfully";
                _logger.LogInformation("Successfully created a new insurance plan.");
                return StatusCode(201, responseML);
            }
            catch (InsurancePlanException ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new insurance plan.");
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpGet("GetAllInsurancePlan")]
        public async Task<ActionResult> GetAllInsurancePlanAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all insurance plans.");
                var result = await insurancePlanBL.GetAllInsurancePlanAsync();

                responseML.Success = true;
                responseML.Message = "Insurance Plan Fetched Successfully";
                responseML.Data = result;
                _logger.LogInformation("Successfully fetched all insurance plans.");
                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all insurance plans.");
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpGet("GetInsurancePlanById/{Id}")]
        public async Task<ActionResult> GetInsurancePlanByIdAsync(int Id)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch insurance plan with Id {InsurancePlanId}.", Id);
                var result = await insurancePlanBL.GetInsurancePlanByIdAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Insurance Plan ID : {Id} Fetched Successfully";
                responseML.Data = result;
                _logger.LogInformation("Successfully fetched insurance plan with Id {InsurancePlanId}.", Id);
                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching insurance plan with Id {InsurancePlanId}.", Id);
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpPut("UpdateInsurancePlanById/{Id}")]
        public async Task<ActionResult> UpdateInsurancePlanByIdAsync(int Id, InsurancePlanML model)
        {
            try
            {
                _logger.LogInformation("Attempting to update insurance plan with Id {InsurancePlanId}.", Id);
                await insurancePlanBL.UpdateInsurancePlanByIdAsync(Id, model);

                responseML.Success = true;
                responseML.Message = $"Insurance Plan ID : {Id} Updated Successfully";
                _logger.LogInformation("Successfully updated insurance plan with Id {InsurancePlanId}.", Id);
                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                _logger.LogError(ex, "Error occurred while updating insurance plan with Id {InsurancePlanId}.", Id);
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpDelete("DeleteInsurancePlanById/{Id}")]
        public async Task<ActionResult> DeleteInsurancePlanByIdAsync(int Id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete insurance plan with Id {InsurancePlanId}.", Id);
                await insurancePlanBL.DeleteInsurancePlanByIdAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Insurance Plan ID : {Id} Deleted Successfully";
                _logger.LogInformation("Successfully deleted insurance plan with Id {InsurancePlanId}.", Id);
                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                _logger.LogError(ex, "Error occurred while deleting insurance plan with Id {InsurancePlanId}.", Id);
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }
    }
}
