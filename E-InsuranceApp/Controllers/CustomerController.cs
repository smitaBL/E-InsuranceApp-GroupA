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
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBL customerBL;
        private readonly ResponseML responseML;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerBL customerBL, ILogger<CustomerController> logger)
        {
            this.customerBL = customerBL;
            this.responseML = new ResponseML();
            this._logger = logger;
        }

        [HttpPost("Register/Customer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomerAsync(CustomerML model)
        {
            try
            {
                _logger.LogInformation($"Attempting to register a new customer {model.Email}");

                await customerBL.RegisterAsync(model);
                
                    responseML.Success = true;
                    responseML.Message = "Customer Created Successfully";
                    
            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error occurred while registering a new customer.");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(201, responseML);
        }

        [HttpGet("Customer/GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all customers.");

                var result = await customerBL.GetAllCustomerAsync();
                if (result != null)
                {
                    responseML.Success = true;
                    responseML.Message = "Customer Fetched Successfully";
                    responseML.Data = result;
                }

            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all customers.");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpGet("Customer/GetCustomerById")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch customer with Id {CustomerId}.", id);

                var result = await customerBL.GetCustomerByIdAsync(id);
                if (result != null)
                {
                    responseML.Success = true;
                    responseML.Message = "Customer Fetched Successfully";
                    responseML.Data = result;
                }
            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customer with Id {CustomerId}.", id);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpGet("Customer/GetCustomerByAgentId")]
        public async Task<IActionResult> GetCustomerByAgentIdAsync(int agentid)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch customers for agent with Id {AgentId}.", agentid);

                var result = await customerBL.GetCustomerByAgentIdAsync(agentid);
                if (result != null)
                {
                    responseML.Success = true;
                    responseML.Message = "Customer Fetched Successfully";
                    responseML.Data = result;
                }
            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers for agent with Id {AgentId}.", agentid);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpDelete("Customer/DeleteCustomerById")]
        public async Task<IActionResult> DeleteCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete customer with Id {CustomerId}.", id);

                await customerBL.DeleteCustomerByIdAsync(id);
                responseML.Success = true;
                responseML.Message = "Customer Deleted Successfully";
                
            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error occurred while deleting customer with Id {CustomerId}.", id);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }

        [HttpPut("Customer/UpdateCustomerById")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerByIdAsync(int id, CustomerML model)
        {
            try
            {
                _logger.LogInformation("Attempting to update customer with Id {CustomerId}.", id);

                await customerBL.UpdateCustomerByIdAsync(id,model);
                responseML.Success = true;
                responseML.Message = "Customer Updated Successfully";

            }
            catch (CustomerException ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer with Id {CustomerId}.", id);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }
    }
}
