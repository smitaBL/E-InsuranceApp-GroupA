using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBL employeeBL;
        private readonly ResponseML responseML;
        public EmployeeController( IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
            this.responseML = new ResponseML();
        }
        [HttpPost("Register/Employee")]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeML employeeEntity)
        {
            try
            {
                var agent = await employeeBL.CreateEmployeeAsync(employeeEntity);
                responseML.Success = true;
                responseML.Data = agent;
                responseML.Message = "Employee created successfully";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                var result = new ResponseML
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(400, result);
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
