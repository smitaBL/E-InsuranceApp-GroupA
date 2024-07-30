using BusinessLayer.Interface;
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
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanBL insurancePlanBL;
        private readonly ResponseML responseML;

        public InsurancePlanController(IInsurancePlanBL insurancePlanBL)
        {
            this.insurancePlanBL = insurancePlanBL;
            responseML = new ResponseML();
        }

        [HttpPost("CreateInsurancePlan")]
        public async Task<ActionResult> CreateInsurancePlan(InsurancePlanML model)
        {
            try
            {
                await insurancePlanBL.CreateInsurancePlan(model);

                responseML.Success = true;
                responseML.Message = "Insurance Plan Added Successfully";

                return StatusCode(201, responseML);
            }
            catch (InsurancePlanException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }


        [HttpGet("GetAllInsurancePlan")]
        public async Task<ActionResult> GetAllInsurancePlanAsync()
        {
            try
            {
                var result = await insurancePlanBL.GetAllInsurancePlanAsync();

                responseML.Success = true;
                responseML.Message = "Insurance Plan Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpGet("GetInsurancePlanById/{Id}")]
        public async Task<ActionResult> GetInsurancePlanByIdAsync(int Id)
        {
            try
            {
                var result = await insurancePlanBL.GetInsurancePlanByIdAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Insurance Plan ID : {Id} Fetched Successfully";
                responseML.Data = result;

                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpPut("UpdateInsurancePlanById/{Id}")]
        public async Task<ActionResult> UpdateInsurancePlanByIdAsync(int Id, InsurancePlanML model)
        {
            try
            {
                await insurancePlanBL.UpdateInsurancePlanByIdAsync(Id, model);

                responseML.Success = true;
                responseML.Message = $"Insurance Plan ID : {Id} Updated Successfully";

                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }

        [HttpDelete("DeleteInsurancePlanById/{Id}")]
        public async Task<ActionResult> DeleteInsurancePlanByIdAsync(int Id)
        {
            try
            {
                await insurancePlanBL.DeleteInsurancePlanByIdAsync(Id);

                responseML.Success = true;
                responseML.Message = $"Insurance Plan ID : {Id} Deleted Successfully";

                return StatusCode(200, responseML);
            }
            catch (InsurancePlanException ex)
            {
                responseML.Success = false;
                responseML.Message = ex.Message;

                return StatusCode(500, responseML);
            }
        }
    }
}
