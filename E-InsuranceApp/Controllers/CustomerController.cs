using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBL customerBL;
        private readonly ResponseML responseML;
        public CustomerController(ICustomerBL customerBL)
        {
            this.customerBL = customerBL;
            this.responseML = new ResponseML();
        }

        [HttpPost("Register/Customer")]
        public async Task<IActionResult> RegisterCustomerAsync(CustomerML model)
        {
            try
            {
                await customerBL.RegisterAsync(model);
                
                    responseML.Success = true;
                    responseML.Message = "Customer Created Successfully";
                    
            }
            catch (CustomerException ex)
            {
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
                await customerBL.DeleteCustomerByIdAsync(id);
                responseML.Success = true;
                responseML.Message = "Customer Deleted Successfully";
                
            }
            catch (CustomerException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }
        [HttpPut("Customer/UpdateCustomerById")]
        public async Task<IActionResult> UpdateCustomerByIdAsync(int id, CustomerML model)
        {
            try
            {
                await customerBL.UpdateCustomerByIdAsync(id,model);
                responseML.Success = true;
                responseML.Message = "Customer Updated Successfully";

            }
            catch (CustomerException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(200, responseML);
        }
    }
}
