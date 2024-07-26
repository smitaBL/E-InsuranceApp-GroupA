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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class SchemeRL : ISchemeRL
    {
        private readonly EInsuranceDbContext _context;

        public SchemeRL(EInsuranceDbContext context)
        {
            _context = context;
        }

        public async Task CreateSchemeAsync(SchemeEntity model, int employeeId)
        {
            try
            {
                var plan = await _context.InsurancePlans.FirstOrDefaultAsync(p => p.PlanID == model.PlanID);
                if (plan == null)
                {
                    throw new SchemeException($"Insurance Plan ID : {model.PlanID} does not exists! Can't add scheme into it.");          
                }

                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
                if (employee == null)
                {
                    throw new SchemeException($"Employee Id : {employeeId} not found");
                }

                var schemes = await _context.Schemes.Where(s => s.SchemeName.Equals(model.SchemeName)).FirstOrDefaultAsync();
                if (schemes != null)
                {
                    throw new SchemeException($"Scheme is already present");
                }

                var parameters = new[]
                {
                    new SqlParameter("@SchemeName", model.SchemeName),
                    new SqlParameter("@SchemeDetails", model.SchemeDetails),
                    new SqlParameter("@SchemePrice", model.SchemePrice),
                    new SqlParameter("@SchemeCover", model.SchemeCover),
                    new SqlParameter("@SchemeTenure", model.SchemeTenure),
                    new SqlParameter("@PlanID", model.PlanID),
                    new SqlParameter("@CreatedAt",model.CreatedAt)
                };

                var result = await _context.Schemes
                                           .FromSqlRaw("EXEC CreateScheme @SchemeName, @SchemeDetails, @SchemePrice, @SchemeCover, @SchemeTenure, @PlanID, @CreatedAt", parameters)
                                           .ToListAsync();

                var scheme =  result.FirstOrDefault();

                //searching for employee and add scheme in the employeeScheme table
                EmployeeSchemeEntity employeeSchemeEntity = new() {
                    SchemeID = scheme.SchemeID,
                    EmployeeID = employee.EmployeeID
                };

                await _context.EmployeeSchemes.AddAsync(employeeSchemeEntity);
                await _context.SaveChangesAsync();
            }
            catch(SchemeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SchemeException(ex.Message);
            }
        }

        public async Task DeleteSchemeAsync(int id)
        {
            try
            {
                var schemeIdParameter = new SqlParameter("@SchemeID", id);

                int count = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteScheme @SchemeID", schemeIdParameter);

                if (count == 0) 
                {
                    throw new SchemeException($"Scheme ID : {id} doesnot exists");
                }
            }
            catch(Exception ex)
            {
                throw new SchemeException(ex.Message);
            }
        }

        public async Task UpdateSchemeAsync(int schemeId, SchemeEntity schemeEntity)
        {
            try
            {
                var plan = await _context.InsurancePlans.FirstOrDefaultAsync(p => p.PlanID == schemeEntity.PlanID);
                if (plan == null)
                {
                    throw new SchemeException($"Insurance Plan ID : {schemeEntity.PlanID} does not exists! Can't add scheme into it.");
                }

                var parameters = new[]
                {
                    new SqlParameter("@SchemeID", schemeId),
                    new SqlParameter("@SchemeName", schemeEntity.SchemeName),
                    new SqlParameter("@SchemeDetails", schemeEntity.SchemeDetails),
                    new SqlParameter("@SchemePrice", schemeEntity.SchemePrice),
                    new SqlParameter("@SchemeCover", schemeEntity.SchemeCover),
                    new SqlParameter("@SchemeTenure", schemeEntity.SchemeTenure),
                    new SqlParameter("@PlanID", schemeEntity.PlanID)
                };

                var result = await _context.Schemes
                    .FromSqlRaw("EXEC UpdateScheme @SchemeID, @SchemeName, @SchemeDetails, @SchemePrice, @SchemeCover, @SchemeTenure, @PlanID", parameters)
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    throw new SchemeException("Update failed or Scheme not found");
                }
            }
            catch (Exception ex)
            {
                throw new SchemeException(ex.Message);
            }
        }


        public async Task<List<SchemeWithInsurancePlanML>> GetAllSchemeAsync()
        {
            try
            {
                var result = await _context.SchemeWithInsurancePlanML
                               .FromSqlRaw("EXEC GetAllSchemesWithInsurancePlans")
                               .ToListAsync();

                if(result == null || result.Count == 0)
                {
                    throw new SchemeException("Schemes not found");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SchemeException(ex.Message);
            }
        }

        public async Task<SchemeWithInsurancePlanML> GetSchemeByIdAsync(int Id)
        {
            try
            {
                var result = await _context.SchemeWithInsurancePlanML
                            .FromSqlRaw("EXEC GetSchemeByIdWithInsurancePlan @SchemeID = {0}", Id)
                            .ToListAsync ();

                var scheme = result.FirstOrDefault();

                if( scheme == null || result == null )
                {
                    throw new SchemeException($"Scheme ID : {Id} does not exists");
                }

                return scheme;
            }
            catch (Exception ex)
            {
                throw new SchemeException(ex.Message);
            }
        }
    }
}
