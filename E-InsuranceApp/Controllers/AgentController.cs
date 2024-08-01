using BusinessLayer.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/agent")]
    [ApiController]
    [EnableCors]
    [Authorize(Roles ="Admin, Agent")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentBL agentBL;
        private readonly ResponseML responseML;
        private readonly ILogger<AgentController> _logger;

        public AgentController(IAgentBL agentBL, ILogger<AgentController> logger)
        {
            this.agentBL = agentBL;
            this.responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("Register/Agent")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAgentAsync(InsuranceAgentML insuranceAgentML)
        {
            try
            {
                _logger.LogInformation("Creating a new agent.");
                await agentBL.CreateAgentAsync(insuranceAgentML);
                responseML.Success = true;
                responseML.Message = "Agent created successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
                _logger.LogError($"Error creating agent: {ex.Message} for agent {insuranceAgentML.Email}");
                responseML.Success = false;
                responseML.Message = ex.Message;        
                return StatusCode(400,responseML);
            }
        }

        [HttpPut("Update/Agent")]
        public async Task<IActionResult> UpdateAgentAsync(int id, InsuranceAgentML insuranceAgentML)
        {
            try
            {
                _logger.LogInformation($"Updating agent with Id {id}.");
                await agentBL.UpdateAgentAsync(id,insuranceAgentML);
                responseML.Success = true;
                responseML.Message = $"Agent with Id {id} updated successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
                _logger.LogError($"Error updating agent with Id {id}: {ex.Message}");
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("GetById/Agent")]
        public async Task<IActionResult> GetByIdAgentAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching details for agent with Id {id}.");

                var result =await agentBL.GetByIdAgentAsync(id);
                responseML.Success = true;
                responseML.Data= result;
                responseML.Message = $"Agent with Id {id} : ";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
                _logger.LogError($"Error fetching details for agent with Id {id}: {ex.Message}");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("GetAll/Agent")]
        public async Task<IActionResult> GetAllAgentAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all agents.");

                var result =await agentBL.GetAllAgentAsync();
                
                return StatusCode(200,result);
            }
            catch (AgentException ex)
            {
                _logger.LogError($"Error fetching all agents: {ex.Message}");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpDelete("Delete/Agent")]
        public async Task<IActionResult> DeleteAgentAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting agent with Id {id}.");

                await agentBL.DeleteAgentAsync(id);
                responseML.Success = true;
                responseML.Message = $"Agent with Id {id} deleted successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
                _logger.LogError($"Error deleting agent with Id {id}: {ex.Message}");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }
    }
}
