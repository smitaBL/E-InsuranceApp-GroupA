using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Service;

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
        public PolicyController(IPolicyBL policyBL)
        {
            this.policyBL = policyBL;
            responseML = new ResponseML();
        }

        [HttpPost("Policy/AddPolicy")]
        public async Task<IActionResult> AddPolicyAsync(PolicyML model)
        {
            try
            {
                var customerid = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                await policyBL.AddPolicyAsync(customerid,model);

                responseML.Success = true;
                responseML.Message = "Policy Created Successfully";
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(201, responseML);
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

                }
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
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

                }
            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpDelete("Policy/DeleteById")]
        public async Task<IActionResult> DeletePolicyByIdAsync(int id)
        {
            try
            {
                await policyBL.DeletePolicyByIdAsync(id);

                responseML.Success = true;
                responseML.Message = "Policy Deleted Successfully";

            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpPut("Policy/UpdateById")]
        public async Task<IActionResult> UpdateAdminByIdAsync(int id, PolicyML model)
        {
            try
            {
                var customerid = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                await policyBL.UpdatePolicyByIdAsync(id, customerid, model);

                responseML.Success = true;
                responseML.Message = "Policy Updated Successfully";

            }
            catch (PolicyException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

    }
}
