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
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Service
{
    public class AgentRL : IAgentRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService _rabbitMQService;
        private readonly ILogger<AgentRL> _logger;

        public AgentRL(EInsuranceDbContext context, RabbitMQService rabbitMQService, ILogger<AgentRL> logger)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
            _logger = logger;
        }

        public async Task CreateAgentAsync(InsuranceAgentEntity insuranceAgentML)
        {
            try
            {
                _logger.LogInformation("Creating agent with email: {Email}", insuranceAgentML.Email);

                await _context.InsuranceAgents.AddAsync(insuranceAgentML);
                await _context.SaveChangesAsync();

                var emailML = new EmailML
                {
                    Name = insuranceAgentML.Username,
                    Email = insuranceAgentML.Email,
                    Password = insuranceAgentML.Password
                };
                _rabbitMQService.SendProductMessage(insuranceAgentML);

                _logger.LogInformation("Created agent with email: {Email} successfully", insuranceAgentML.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating agent with email: {Email}", insuranceAgentML.Email);
                throw new AgentException(ex.Message);
            }
        }

        public async Task DeleteAgentAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting agent with ID: {AgentID}", id);

                var user = await _context.Database
                                         .ExecuteSqlRawAsync("EXEC spDeleteAgent @AgentID = {0}", id);

                if (user == 0)
                {
                    _logger.LogWarning("Agent with ID: {AgentID} doesn't exist", id);
                    throw new AgentException("Agent doesn't exist");
                }

                _logger.LogInformation("Deleted agent with ID: {AgentID} successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting agent with ID: {AgentID}", id);
                throw new AgentException(ex.Message);
            }
        }

        public async Task<List<InsuranceAgentEntity>> GetAllAgentAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all agents");

                var agents = await _context.InsuranceAgents
                                           .FromSqlRaw("EXEC spGetAllAgents")
                                           .ToListAsync();

                _logger.LogInformation("Fetched all agents successfully");
                return agents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching all agents");
                throw new AgentException(ex.Message);
            }
        }

        public async Task<InsuranceAgentEntity> GetByIdAgentAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching agent with ID: {AgentID}", id);

                var agent = await _context.InsuranceAgents
                                          .FromSqlRaw("EXEC spGetAgentById @AgentID = {0}", id)
                                          .ToListAsync();
                var result = agent.FirstOrDefault();

                if (result == null)
                {
                    _logger.LogWarning("Agent with ID: {AgentID} doesn't exist", id);
                    throw new AgentException("Agent doesn't exist");
                }

                _logger.LogInformation("Fetched agent with ID: {AgentID} successfully", id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching agent with ID: {AgentID}", id);
                throw new AgentException(ex.Message);
            }
        }

        public async Task UpdateAgentAsync(int id, InsuranceAgentEntity insuranceAgent)
        {
            try
            {
                _logger.LogInformation("Updating agent with ID: {AgentID}", id);

                var existingAgent = await _context.InsuranceAgents
                                                  .FirstOrDefaultAsync(a => a.AgentID == id);
                if (existingAgent == null)
                {
                    _logger.LogWarning("Agent with ID: {AgentID} doesn't exist", id);
                    throw new AgentException("Agent doesn't exist");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spUpdateAgent @AgentID = {0}, @FullName = {1}, @Username = {2}, @Email = {3}, @Password = {4}",
                    id, insuranceAgent.FullName, insuranceAgent.Username, insuranceAgent.Email, insuranceAgent.Password
                );
                _rabbitMQService.SendProductMessage(insuranceAgent);

                _logger.LogInformation("Updated agent with ID: {AgentID} successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating agent with ID: {AgentID}", id);
                throw new AgentException(ex.Message);
            }
        }
    }
}
