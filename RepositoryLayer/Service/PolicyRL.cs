using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class PolicyRL : IPolicyRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly ILogger<PolicyRL> _logger;

        public PolicyRL(EInsuranceDbContext context, ILogger<PolicyRL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreatePolicyAsync(PolicyEntity policy)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@CustomerID", policy.CustomerID),
                    new SqlParameter("@SchemeID", policy.SchemeID),
                    new SqlParameter("@PolicyDetails", policy.PolicyDetails),
                    new SqlParameter("@Premium", policy.Premium),
                    new SqlParameter("@DateIssued", policy.DateIssued),
                    new SqlParameter("@MaturityPeriod", policy.MaturityPeriod)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "Exec sp_AddPolicy @CustomerID, @SchemeID, @PolicyDetails, @Premium, @DateIssued, @MaturityPeriod",
                    parameters);

                _logger.LogInformation("Policy created successfully for CustomerID: {CustomerID}", policy.CustomerID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating policy for CustomerID: {CustomerID}", policy.CustomerID);
                throw new PolicyException(ex.Message);
            }
        }

        public async Task DeletePolicyAsync(int id)
        {
            try
            {
                var policy = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeletePolicyById @Id = {0}", id);

                if (policy == 0)
                {
                    _logger.LogWarning("Policy not found with ID: {PolicyID}", id);
                    throw new PolicyException("Policy not found");
                }

                _logger.LogInformation("Policy deleted successfully with ID: {PolicyID}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting policy with ID: {PolicyID}", id);
                throw new PolicyException(ex.Message);
            }
        }

        public async Task<List<PolicyDTO>> GetAllPoliciesAsync(int customerid)
        {
            try
            {
                var policies = await _context.PolicyDTOs
                    .FromSqlRaw("EXEC sp_GetAllPolicies @CustomerID={0}", customerid)
                    .ToListAsync();

                if (!policies.Any())
                {
                    _logger.LogWarning("No policies found for CustomerID: {CustomerID}", customerid);
                    throw new PolicyException("Policy not found");
                }

                _logger.LogInformation("Retrieved {Count} policies for CustomerID: {CustomerID}", policies.Count, customerid);
                return policies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving policies for CustomerID: {CustomerID}", customerid);
                throw new PolicyException(ex.Message);
            }
        }

        public async Task<PolicyDTO> GetPolicyByIdAsync(int id)
        {
            try
            {
                var policies = await _context.PolicyDTOs
                    .FromSqlRaw("EXEC sp_GetPolicyById @Id={0}", id)
                    .ToListAsync();

                var policy = policies.FirstOrDefault();

                if (policy == null)
                {
                    _logger.LogWarning("Policy not found with ID: {PolicyID}", id);
                    throw new PolicyException("Policy not found");
                }

                _logger.LogInformation("Policy retrieved successfully with ID: {PolicyID}", id);
                return policy;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving policy with ID: {PolicyID}", id);
                throw new PolicyException(ex.Message);
            }
        }

        public async Task<List<PolicyDTO>> GetPolicyByNameAsync(string customername)
        {
            try
            {
                var policies = await _context.PolicyDTOs
                    .FromSqlRaw("EXEC sp_GetPoliciesByCustomerName @CustomerName={0}", customername)
                    .ToListAsync();

                if (!policies.Any())
                {
                    _logger.LogWarning("No policies found for CustomerName: {CustomerName}", customername);
                    throw new PolicyException("Policy not found");
                }

                _logger.LogInformation("Retrieved {Count} policies for CustomerName: {CustomerName}", policies.Count, customername);
                return policies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving policies for CustomerName: {CustomerName}", customername);
                throw new PolicyException($"Error retrieving policies: {ex.Message}");
            }
        }

        public async Task UpdatePolicyAsync(int id, PolicyEntity policy)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@PolicyID", id),
                    new SqlParameter("@CustomerID", policy.CustomerID),
                    new SqlParameter("@SchemeID", policy.SchemeID),
                    new SqlParameter("@PolicyDetails", policy.PolicyDetails),
                    new SqlParameter("@Premium", policy.Premium),
                    new SqlParameter("@DateIssued", policy.DateIssued),
                    new SqlParameter("@MaturityPeriod", policy.MaturityPeriod),
                    new SqlParameter("@PolicyLapseDate", policy.PolicyLapseDate)
                };

                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdatePolicyById @PolicyID, @CustomerID, @SchemeID, @PolicyDetails, @Premium, @DateIssued, @MaturityPeriod, @PolicyLapseDate",
                    parameters);

                if (result == 0)
                {
                    _logger.LogWarning("Policy not found or could not be updated with ID: {PolicyID}", id);
                    throw new PolicyException("Policy not found or could not be updated");
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Policy updated successfully with ID: {PolicyID}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating policy with ID: {PolicyID}", id);
                throw new PolicyException(ex.Message);
            }
        }
    }
}
