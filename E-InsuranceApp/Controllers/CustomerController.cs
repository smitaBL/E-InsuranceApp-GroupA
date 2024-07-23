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
                var result = await customerBL.RegisterAsync(model);
                if (result != null)
                {

                    responseML.Success = true;
                    responseML.Message = "Customer Created Successfully";
                    responseML.Data = result;

                }
            }
            catch (CustomerException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }

            return StatusCode(201, responseML);
        }
    }
}
