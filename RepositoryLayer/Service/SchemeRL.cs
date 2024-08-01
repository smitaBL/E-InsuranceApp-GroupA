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
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class SchemeRL : ISchemeRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly ILogger<SchemeRL> _logger;

        public SchemeRL(EInsuranceDbContext context, ILogger<SchemeRL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateSchemeAsync(SchemeEntity model, int employeeId)
        {
            try
            {
                var plan = await _context.InsurancePlans.FirstOrDefaultAsync(p => p.PlanID == model.PlanID);
                if (plan == null)
                {
                    _logger.LogWarning("Insurance Plan ID: {PlanID} does not exist. Can't add scheme into it.", model.PlanID);
                    throw new SchemeException($"Insurance Plan ID : {model.PlanID} does not exist! Can't add scheme into it.");
                }

                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
                if (employee == null)
                {
                    _logger.LogWarning("Employee ID: {EmployeeID} not found.", employeeId);
                    throw new SchemeException($"Employee ID : {employeeId} not found");
                }

                var schemes = await _context.Schemes.Where(s => s.SchemeName.Equals(model.SchemeName)).FirstOrDefaultAsync();
                if (schemes != null)
                {
                    _logger.LogWarning("Scheme with name: {SchemeName} is already present.", model.SchemeName);
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
                    new SqlParameter("@CreatedAt", model.CreatedAt)
                };

                var result = await _context.Schemes
                    .FromSqlRaw("EXEC CreateScheme @SchemeName, @SchemeDetails, @SchemePrice, @SchemeCover, @SchemeTenure, @PlanID, @CreatedAt", parameters)
                    .ToListAsync();

                var scheme = result.FirstOrDefault();

                EmployeeSchemeEntity employeeSchemeEntity = new()
                {
                    SchemeID = scheme.SchemeID,
                    EmployeeID = employee.EmployeeID
                };

                await _context.EmployeeSchemes.AddAsync(employeeSchemeEntity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Scheme created successfully for EmployeeID: {EmployeeID}", employeeId);
            }
            catch (SchemeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating scheme for EmployeeID: {EmployeeID}", employeeId);
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
                    _logger.LogWarning("Scheme ID: {SchemeID} does not exist.", id);
                    throw new SchemeException($"Scheme ID : {id} does not exist");
                }

                _logger.LogInformation("Scheme deleted successfully with ID: {SchemeID}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting scheme with ID: {SchemeID}", id);
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
                    _logger.LogWarning("Insurance Plan ID: {PlanID} does not exist. Can't update scheme.", schemeEntity.PlanID);
                    throw new SchemeException($"Insurance Plan ID : {schemeEntity.PlanID} does not exist! Can't update scheme.");
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
                    _logger.LogWarning("Update failed or Scheme not found with ID: {SchemeID}", schemeId);
                    throw new SchemeException("Update failed or Scheme not found");
                }

                _logger.LogInformation("Scheme updated successfully with ID: {SchemeID}", schemeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating scheme with ID: {SchemeID}", schemeId);
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

                if (result == null || result.Count == 0)
                {
                    _logger.LogWarning("No schemes found.");
                    throw new SchemeException("Schemes not found");
                }

                _logger.LogInformation("Retrieved {Count} schemes.", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all schemes.");
                throw new SchemeException(ex.Message);
            }
        }

        public async Task<SchemeWithInsurancePlanML> GetSchemeByIdAsync(int id)
        {
            try
            {
                var result = await _context.SchemeWithInsurancePlanML
                    .FromSqlRaw("EXEC GetSchemeByIdWithInsurancePlan @SchemeID = {0}", id)
                    .ToListAsync();

                var scheme = result.FirstOrDefault();

                if (scheme == null)
                {
                    _logger.LogWarning("Scheme ID: {SchemeID} does not exist.", id);
                    throw new SchemeException($"Scheme ID : {id} does not exist");
                }

                _logger.LogInformation("Scheme retrieved successfully with ID: {SchemeID}", id);
                return scheme;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving scheme with ID: {SchemeID}", id);
                throw new SchemeException(ex.Message);
            }
        }
    }
}
