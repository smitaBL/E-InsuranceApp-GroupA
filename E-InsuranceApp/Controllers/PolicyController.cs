using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyBL policyBL;
        private readonly ResponseML responseML;
        private readonly ILogger<PolicyController> _logger;

        public PolicyController(IPolicyBL policyBL, ILogger<PolicyController> logger)
        {
            this.policyBL = policyBL;
            responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("Policy/AddPolicy")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddPolicyAsync(PolicyML model)
        {
            try
            {
                var customerid = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                await policyBL.AddPolicyAsync(customerid, model);
                responseML.Success = true;
                responseML.Message = "Policy Created Successfully";

                _logger.LogInformation($"Policy created successfully for customer ID: {customerid}");
                return StatusCode(201, responseML);
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError($"Error creating policy: {ex.Message}");
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("Policy/GetALL")]
        public async Task<IActionResult> GetAllPoliciesAsync()
        {
            try
            {
                var customerid = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var result = await policyBL.GetAllPoliciesAsync(customerid);
                if (result != null)
                {
                    responseML.Success = true;
                    responseML.Message = "Policies Fetched Successfully";
                    responseML.Data = result;

                    _logger.LogInformation($"All policies fetched successfully for customer ID: {customerid}");
                }
                return StatusCode(200, responseML);
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError($"Error fetching all policies: {ex.Message}");
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("Policy/GetById")]
        public async Task<IActionResult> GetPolicyByIdAsync(int id)
        {
            try
            {
                var result = await policyBL.GetPolicyByIdAsync(id);
                if (result != null)
                {
                    responseML.Success = true;
                    responseML.Message = "Policy Fetched Successfully";
                    responseML.Data = result;

                    _logger.LogInformation($"Policy with ID: {id} fetched successfully");
                }
                return StatusCode(200, responseML);
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError($"Error fetching policy with ID: {id}: {ex.Message}");
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("Policy/GetByName")]
        public async Task<IActionResult> GetPolicyByNameAsync(string customername)
        {
            try
            {
                var result = await policyBL.GetPolicyByNameAsync(customername);
                if (result != null)
                {
                    responseML.Success = true;
                    responseML.Message = "Policy Fetched Successfully";
                    responseML.Data = result;

                    _logger.LogInformation($"Policy fetched successfully for customer name: {customername}");
                }
                return StatusCode(200, responseML);
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError($"Error fetching policy for customer name: {customername}: {ex.Message}");
                return StatusCode(400, responseML);
            }
        }

        [HttpDelete("Policy/DeleteById")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> DeletePolicyByIdAsync(int PolicyId)
        {
            try
            {
                await policyBL.DeletePolicyByIdAsync(PolicyId);

                responseML.Success = true;
                responseML.Message = "Policy Deleted Successfully";

                _logger.LogInformation($"Policy with ID: {PolicyId} deleted successfully");
                return StatusCode(200, responseML);
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError($"Error deleting policy with ID: {PolicyId}: {ex.Message}");
                return StatusCode(400, responseML);
            }
        }

        [HttpPut("Policy/UpdateById")]
        [Authorize(Roles = "Customer,Agent")]
        public async Task<IActionResult> UpdatePolicyByIdAsync(int id, PolicyML model)
        {
            try
            {
                var customerid = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                await policyBL.UpdatePolicyByIdAsync(id, customerid, model);

                responseML.Success = true;
                responseML.Message = "Policy Updated Successfully";

                _logger.LogInformation($"Policy with ID: {id} updated successfully");
                return StatusCode(200, responseML);
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                _logger.LogError($"Error updating policy with ID: {id}: {ex.Message}");
                return StatusCode(400, responseML);
            }
        }
    }
}
