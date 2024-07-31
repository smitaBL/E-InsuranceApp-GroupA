using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class PolicyRL : IPolicyRL
    {
        private readonly EInsuranceDbContext _context;

        public PolicyRL(EInsuranceDbContext context)
        {
            _context = context;
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
                    new SqlParameter("@MaturityPeriod", policy.MaturityPeriod),
                    //new SqlParameter("@PolicyLapseDate", policy.PolicyLapseDate)
                };

                await _context.Database.ExecuteSqlRawAsync(
                            "Exec sp_AddPolicy @CustomerID, @SchemeID, @PolicyDetails, @Premium, @DateIssued, @MaturityPeriod",
                            parameters);
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
            
        }

        public async Task DeletePolicyAsync(int id)
        {
            try
            {
                var policy = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeletePolicyById @Id = {0}", id);

                if (policy == null)
                {
                    throw new PolicyException("Policy not found");
                }
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }

        public async Task<List<PolicyDTO>> GetAllPoliciesAsync(int customerid)
        {
            try
            {
                var policies = await _context.Policies
            .FromSqlRaw("EXEC sp_GetAllPolicies @CustomerID={0}", customerid)
            .ToListAsync();

                if (policies == null || !policies.Any())
                {
                    throw new PolicyException("Policy not found");
                }

                return policies.Select(p => new PolicyDTO
                {
                    PolicyID = p.PolicyID,
                    CustomerID = p.CustomerID,
                    SchemeID = p.SchemeID,
                    PolicyDetails = p.PolicyDetails,
                    Premium = p.Premium,
                    DateIssued = p.DateIssued,
                    MaturityPeriod = p.MaturityPeriod,
                    PolicyLapseDate = p.PolicyLapseDate,
                    CreatedAt = p.CreatedAt,
                    Status = _context.PolicyStatus.Where(ps => ps.PolicyID == p.PolicyID).Select(ps => ps.Status).FirstOrDefault()
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }

public async Task<PolicyDTO> GetPolicyByIdAsync(int id)
        {
            try
            {
                var policies = await _context.Policies
                    .FromSqlRaw("EXEC sp_GetPolicyById @Id = {0}", id)
                    .ToListAsync();
                

                var policy = policies.FirstOrDefault();

                if (policy == null)
                {
                    throw new PolicyException("Policy not found");
                }
                return policies.Select(p => new PolicyDTO
                {
                    PolicyID = p.PolicyID,
                    CustomerID = p.CustomerID,
                    SchemeID = p.SchemeID,
                    PolicyDetails = p.PolicyDetails,
                    Premium = p.Premium,
                    DateIssued = p.DateIssued,
                    MaturityPeriod = p.MaturityPeriod,
                    PolicyLapseDate = p.PolicyLapseDate,
                    CreatedAt = p.CreatedAt,
                    Status = _context.PolicyStatus.Where(ps => ps.PolicyID == p.PolicyID).Select(ps => ps.Status).FirstOrDefault()
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }

        public async Task UpdatePolicyAsync(int id, PolicyEntity policy)
        {
            try
            {
                var policies = await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdatePolicyById @PolicyID = {0}, @CustomerID = {1}, @SchemeID = {2}, @PolicyDetails = {3}, @Premium = {4}, @DateIssued = {5}, @MaturityPeriod = {6}, @PolicyLapseDate = {7}",
                id, policy.CustomerID, policy.SchemeID, policy.PolicyDetails, policy.Premium, policy.DateIssued, policy.MaturityPeriod, policy.PolicyLapseDate);

                if (policies == 0)
                {
                    throw new PolicyException("Policy not found or could not be updated");
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new PolicyException(ex.Message);
            }
        }
    }
}
