using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public InsurancePlanRL(EInsuranceDbContext context)
        {
            _context = context;
        }

        public async Task CreateInsurancePlan(InsurancePlanEntity model)
        {
            try
            {
                var planID = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC CreateInsurancePlan 
                    @PlanName = {model.PlanName}, 
                    @PlanDetails = {model.PlanDetails}, 
                    @CreatedAt = {model.CreatedAt}");

                Console.WriteLine(planID);
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }

        public async Task DeleteInsurancePlanByIdAsync(int id)
        {
            try
            {
                var PlanId = new SqlParameter("@PlanID", id);

                int count = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteInsurancePlanById @PlanID", PlanId);

                if (count == 0) { throw new InsurancePlanException($"InsurancePlan Id : {id} does not exists"); }

            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }

        public async Task<List<InsurancePlanEntity>> GetAllInsurancePlanAsync()
        {
            try
            {
                var insurancePLanList =  await _context.InsurancePlans.FromSqlRaw("EXEC GetAllInsurancePlan").ToListAsync();

                if( insurancePLanList.Count == 0 )
                {
                    throw new InsurancePlanException("Insurance Plans does not exists");
                }

                return insurancePLanList;
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }

        public async Task<InsurancePlanEntity> GetInsurancePlanByIdAsync(int id)
        {
            try
            {
                var InsurancePlans = await _context.InsurancePlans
                                                  .FromSqlRaw("EXEC GetInsurancePlanById @PlanID", new SqlParameter("@PlanID", id))
                                                  .ToListAsync();

                var InsurancePlan = InsurancePlans.FirstOrDefault();

                if (InsurancePlan == null)
                {
                    throw new InsurancePlanException($"InsurancePlan Id : {id} does not exists");
                }

                return InsurancePlan;
            }
            catch(InsurancePlanException)
            {
                throw;
            }
        }

        public async Task UpdateInsurancePlanByIdAsync(int id, InsurancePlanEntity insurancePlanEntity)
        {
            try
            {
                var parameters = new[] 
                {
                    new SqlParameter("@PlanID", id),
                    new SqlParameter("@PlanName", insurancePlanEntity.PlanName),
                    new SqlParameter("@PlanDetails", insurancePlanEntity.PlanDetails)
                };

                int count = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateInsurancePlan @PlanID, @PlanName, @PlanDetails", parameters);
                
                if(count == 0) { throw new InsurancePlanException($"InsurancePlan Id : {id} does not exists"); }
            }
            catch (InsurancePlanException)
            {
                throw;
            }
        }
    }
}
