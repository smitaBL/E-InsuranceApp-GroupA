using BusinessLayer.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentBL agentBL;
        private readonly ResponseML responseML;
        public AgentController(IAgentBL agentBL)
        {
            this.agentBL = agentBL;
            this.responseML = new ResponseML();
        }
        [HttpPost("Register/Agent")]
        public async Task<IActionResult> CreateAgentAsync(InsuranceAgentML insuranceAgentML)
        {
            try
            {
                await agentBL.CreateAgentAsync(insuranceAgentML);
                responseML.Success = true;
                responseML.Message = "Agent created successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
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
                await agentBL.UpdateAgentAsync(id,insuranceAgentML);
                responseML.Success = true;
                responseML.Message = $"Agent with Id {id} updated successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
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
                var result=await agentBL.GetByIdAgentAsync(id);
                responseML.Success = true;
                responseML.Data= result;
                responseML.Message = $"Agent with Id {id} : ";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
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
                var result=await agentBL.GetAllAgentAsync();
                //responseML.Success = true;
                //responseML.Data= result;
                //responseML.Message = "All Agents : ";
                return StatusCode(200,result);
                //return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
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
                await agentBL.DeleteAgentAsync(id);
                responseML.Success = true;
                responseML.Message = $"Agent with Id {id} deleted successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }
    }
}
