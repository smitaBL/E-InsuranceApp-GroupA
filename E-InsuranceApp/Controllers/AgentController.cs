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
                var agent=await agentBL.CreateAgentAsync(insuranceAgentML);
                responseML.Success = true;
                responseML.Data= agent;
                responseML.Message = "Agent created successfully";
                return StatusCode(201, responseML);
            }
            catch (AgentException ex)
            {
                var result = new ResponseML
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(400,result);
            }
            catch (Exception ex)
            {
                var result = new ResponseML
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
    }
}
