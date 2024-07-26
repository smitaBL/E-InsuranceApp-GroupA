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
    public class AgentRL : IAgentRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService rabbitMQService;
        public AgentRL(EInsuranceDbContext context,RabbitMQService rabbitMQService)
        {
            this._context = context;
            this.rabbitMQService = rabbitMQService;
        }
        public async Task CreateAgentAsync(InsuranceAgentEntity insuranceAgentML)
        {
            try
            {
                _context.InsuranceAgents.AddAsync(insuranceAgentML);
                await _context.SaveChangesAsync();
                EmailML emailML = new EmailML
                {
                    Name = insuranceAgentML.Username,
                    Email = insuranceAgentML.Email,
                    Password = insuranceAgentML.Password
                };
                rabbitMQService.SendProductMessage(insuranceAgentML);
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }

        public async Task DeleteAgentAsync(int id)
        {
            try
            {
                var user = await _context.Database
                                         .ExecuteSqlRawAsync("EXEC spDeleteAgent @AgentID = {0}", id);

                if (user == null)
                {
                    throw new AgentException("Agent doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }

        public async Task<List<InsuranceAgentEntity>> GetAllAgentAsync()
        {
            try
            {
                var agents = await _context.InsuranceAgents
                                           .FromSqlRaw("EXEC spGetAllAgents")
                                           .ToListAsync();

                return agents;
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }

        public async Task<InsuranceAgentEntity> GetByIdAgentAsync(int id)
        {
            try
            {
                var agent = await _context.InsuranceAgents
                                          .FromSqlRaw("EXEC spGetAgentById @AgentID = {0}", id)
                                          .ToListAsync();
                var result=agent.FirstOrDefault();

                if (agent == null)
                {
                    throw new AgentException("Agent doesn't exist");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }

        public async Task UpdateAgentAsync(int id, InsuranceAgentEntity insuranceAgent)
        {
            try
            {
                var existingAgent = await _context.InsuranceAgents
                                                  .FirstOrDefaultAsync(a => a.AgentID == id);
                if (existingAgent == null)
                {
                    throw new AgentException("Agent doesn't exist");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spUpdateAgent @AgentID = {0}, @FullName = {1},@Username={2}, @Email = {3}, @Password = {4}",
                    id, insuranceAgent.FullName,insuranceAgent.Username,insuranceAgent.Email, insuranceAgent.Password
                );
                rabbitMQService.SendProductMessage(insuranceAgent);
            }
            catch (Exception ex)
            {
                throw new AgentException(ex.Message);
            }
        }
    }
}
