using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;

namespace E_InsuranceApp.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [EnableCors]
    [Authorize(Roles ="Admin,Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBL employeeBL;
        private readonly ResponseML responseML;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController( IEmployeeBL employeeBL, ILogger<EmployeeController> logger)
        {
            this.employeeBL = employeeBL;
            this.responseML = new ResponseML();
            _logger = logger;
        }

        [HttpPost("Register/Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeML employeeEntity)
        {
            try
            {
                _logger.LogInformation($"Attempting to create a new employee {employeeEntity.Email}");

                await employeeBL.CreateEmployeeAsync(employeeEntity);
                responseML.Success = true;
                responseML.Message = "Employee created successfully";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new employee.");

                responseML.Success = false;
                responseML.Message = ex.Message;         
                return StatusCode(400, responseML);
            }
        }

        [HttpPut("Update/Employee")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id,EmployeeML employeeEntity)
        {
            try
            {
                _logger.LogInformation("Attempting to update employee with Id {EmployeeId}.", id);

                await employeeBL.UpdateEmployeeAsync(id,employeeEntity);
                responseML.Success = true;
                responseML.Message = $"Employee with Id {id} updated successfully";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee with Id {EmployeeId}.", id);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("GetAll/Employee")]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all employees.");

                var result =await employeeBL.GetAllEmployeeAsync();
                responseML.Success = true;
                responseML.Data=result;
                responseML.Message = "All Employees : ";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all employees.");

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpGet("GetById/Employee")]
        public async Task<IActionResult> GetByIdEmployeeAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch employee with Id {EmployeeId}.", id);

                var result =await employeeBL.GetByIdEmployeeAsync(id);
                responseML.Success = true;
                responseML.Data=result;
                responseML.Message = $"Employee with Id {id} : ";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employee with Id {EmployeeId}.", id);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }

        [HttpDelete("Delete/Employee")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete employee with Id {EmployeeId}.", id);

                await employeeBL.DeleteEmployeeAsync(id);
                responseML.Success = true;
                responseML.Message = $"Employee with Id {id} deleted successfully : ";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee with Id {EmployeeId}.", id);

                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }
    }
}
