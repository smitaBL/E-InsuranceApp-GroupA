using BusinessLayer.Interface;
using BusinessLayer.Service;
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
    [Authorize(Roles ="Admin, Employee, Agent")]
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionBL commissionBL;
        private readonly ResponseML responseML;
        private readonly ILogger<CommissionController> _logger;

        public CommissionController(ICommissionBL commissionBL, ILogger<CommissionController> logger)
        {
            this.commissionBL = commissionBL;
            this.responseML = new ResponseML();
            this._logger = logger;
        }

        [HttpPost("Add/Commission")]
        public async Task<IActionResult> AddCommissionAsync(CommissionML commissionML)
        {
            try
            {
                _logger.LogInformation("Attempting to add a commission for agent with Id {AgentID}.", commissionML.AgentID);

                await commissionBL.AddCommissionAsync(commissionML);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {commissionML.AgentID} added successfully";
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
                _logger.LogError(ex, "Error occurred while adding commission for agent with Id {AgentID}.", commissionML.AgentID);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpPut("Update/Commission")]
        public async  Task<IActionResult> UpdateCommissionAsync(CommissionML commissionML,float commissionPercent)
        {
            try
            {
                _logger.LogInformation("Attempting to update commission for agent with Id {AgentID}.", commissionML.AgentID);

                await commissionBL.UpdateCommissionAsync(commissionML,commissionPercent);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {commissionML.AgentID} updated successfully";
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
                _logger.LogError(ex, "Error occurred while updating commission for agent with Id {AgentID}.", commissionML.AgentID);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpDelete("Delete/Commission")]
        public async Task<IActionResult> DeleteCommissionAsync(int agentId, int policyId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete commission for agent with Id {AgentID} and policy with Id {PolicyID}.", agentId, policyId);

                await commissionBL.DeleteCommissionAsync(agentId, policyId);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {agentId} deleted successfully";
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
                _logger.LogError(ex, "Error occurred while deleting commission for agent with Id {AgentID} and policy with Id {PolicyID}.", agentId, policyId);


                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("GetAll/Commission")]
        public async Task<IActionResult> GetAllCommissionAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all commissions.");

                var result =await commissionBL.GetAllCommissionAsync();
                responseML.Success = true;
                responseML.Message = "All commissions : ";
                responseML.Data=result;
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all commissions.");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("GetById/Commission")]
        public async Task<IActionResult> GetByIdCommissionAsync(int agentId, int policyId)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch commission for agent with Id {AgentID} and policy with Id {PolicyID}.", agentId, policyId);

                var result =await commissionBL.GetByIdCommissionAsync(agentId, policyId);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {agentId} fetched successfully";
                responseML.Data= result;
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching commission for agent with Id {AgentID} and policy with Id {PolicyID}.", agentId, policyId);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }
    }
}
