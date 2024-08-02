using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class InsurancePlanRL : IInsurancePlanRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly ILogger<InsurancePlanRL> _logger;

        public InsurancePlanRL(EInsuranceDbContext context, ILogger<InsurancePlanRL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateInsurancePlan(InsurancePlanEntity model)
        {
            try
            {
                _logger.LogInformation("Creating insurance plan with details: {@model}", model);

                var planID = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC CreateInsurancePlan 
                    @PlanName = {model.PlanName}, 
                    @PlanDetails = {model.PlanDetails}");

                if (planID == 0)
                {
                    _logger.LogWarning("Failed to create insurance plan: {@model}", model);
                    throw new InsurancePlanException("Error occurred while adding insurance plan");
                }

                _logger.LogInformation("Insurance plan created successfully: {@model}", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating insurance plan: {@model}", model);
                throw new InsurancePlanException(ex.Message);
            }
        }

        public async Task DeleteInsurancePlanByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting insurance plan with ID: {id}", id);

                var PlanId = new SqlParameter("@PlanID", id);

                int count = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteInsurancePlanById @PlanID", PlanId);

                if (count == 0)
                {
                    _logger.LogWarning("Failed to delete insurance plan with ID: {id}. Insurance plan does not exist.", id);
                    throw new InsurancePlanException($"InsurancePlan Id : {id} does not exist");
                }

                _logger.LogInformation("Insurance plan with ID: {id} deleted successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting insurance plan with ID: {id}", id);
                throw new InsurancePlanException(ex.Message);
            }
        }

        public async Task<List<InsurancePlanEntity>> GetAllInsurancePlanAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all insurance plans");

                var insurancePlanList = await _context.InsurancePlans.FromSqlRaw("EXEC GetAllInsurancePlan").ToListAsync();

                if (insurancePlanList.Count == 0)
                {
                    _logger.LogWarning("No insurance plans found");
                    throw new InsurancePlanException("Insurance Plans do not exist");
                }

                _logger.LogInformation("Fetched all insurance plans successfully");
                return insurancePlanList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching all insurance plans");
                throw new InsurancePlanException(ex.Message);
            }
        }

        public async Task<InsurancePlanEntity> GetInsurancePlanByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching insurance plan with ID: {id}", id);

                var insurancePlans = await _context.InsurancePlans
                                                   .FromSqlRaw("EXEC GetInsurancePlanById @PlanID", new SqlParameter("@PlanID", id))
                                                   .ToListAsync();

                var insurancePlan = insurancePlans.FirstOrDefault();

                if (insurancePlan == null)
                {
                    _logger.LogWarning("Insurance plan with ID: {id} does not exist", id);
                    throw new InsurancePlanException($"InsurancePlan Id : {id} does not exist");
                }

                _logger.LogInformation("Fetched insurance plan with ID: {id} successfully", id);
                return insurancePlan;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching insurance plan with ID: {id}", id);
                throw new InsurancePlanException(ex.Message);
            }
        }

        public async Task UpdateInsurancePlanByIdAsync(int id, InsurancePlanEntity insurancePlanEntity)
        {
            try
            {
                _logger.LogInformation("Updating insurance plan with ID: {id} and details: {@insurancePlanEntity}", id, insurancePlanEntity);

                var parameters = new[]
                {
                    new SqlParameter("@PlanID", id),
                    new SqlParameter("@PlanName", insurancePlanEntity.PlanName),
                    new SqlParameter("@PlanDetails", insurancePlanEntity.PlanDetails)
                };

                int count = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateInsurancePlan @PlanID, @PlanName, @PlanDetails", parameters);

                if (count == 0)
                {
                    _logger.LogWarning("Failed to update insurance plan with ID: {id}. Insurance plan does not exist.", id);
                    throw new InsurancePlanException($"InsurancePlan Id : {id} does not exist");
                }

                _logger.LogInformation("Updated insurance plan with ID: {id} successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating insurance plan with ID: {id}", id);
                throw new InsurancePlanException(ex.Message);
            }
        }
    }
}
