using BusinessLayer.Interface;
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
                await employeeBL.CreateEmployeeAsync(employeeEntity);
                responseML.Success = true;
                responseML.Message = "Employee created successfully";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
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
                await employeeBL.UpdateEmployeeAsync(id,employeeEntity);
                responseML.Success = true;
                responseML.Message = $"Employee with Id {id} updated successfully";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
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
                var result=await employeeBL.GetAllEmployeeAsync();
                responseML.Success = true;
                responseML.Data=result;
                responseML.Message = "All Employees : ";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
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
                var result=await employeeBL.GetByIdEmployeeAsync(id);
                responseML.Success = true;
                responseML.Data=result;
                responseML.Message = $"Employee with Id {id} : ";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
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
                await employeeBL.DeleteEmployeeAsync(id);
                responseML.Success = true;
                responseML.Message = $"Employee with Id {id} deleted successfully : ";
                return StatusCode(201, responseML);
            }
            catch (EmployeeException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;
                return StatusCode(400, responseML);
            }
        }
    }
}
