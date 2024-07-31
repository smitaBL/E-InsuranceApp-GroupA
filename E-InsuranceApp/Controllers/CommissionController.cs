using BusinessLayer.Interface;
using BusinessLayer.Service;
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
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionBL commissionBL;
        private readonly ResponseML responseML;
        public CommissionController(ICommissionBL commissionBL)
        {
            this.commissionBL = commissionBL;
            this.responseML = new ResponseML();
        }
        [HttpPost("Add/Commission")]
        public async Task<IActionResult> AddCommissionAsync(CommissionML commissionML)
        {
            try
            {
                await commissionBL.AddCommissionAsync(commissionML);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {commissionML.AgentID} added successfully";
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
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
                await commissionBL.UpdateCommissionAsync(commissionML,commissionPercent);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {commissionML.AgentID} updated successfully";
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
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
                await commissionBL.DeleteCommissionAsync(agentId, policyId);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {agentId} deleted successfully";
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
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
                var result=await commissionBL.GetAllCommissionAsync();
                responseML.Success = true;
                responseML.Message = "All commissions : ";
                responseML.Data=result;
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
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
                var result=await commissionBL.GetByIdCommissionAsync(agentId, policyId);
                responseML.Success = true;
                responseML.Message = $"Commission for agent with Id {agentId} fetched successfully";
                responseML.Data= result;
                return StatusCode(201, responseML);
            }
            catch (CommissionException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }
    }
}
