using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
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
        private readonly EmailService _emailService;
        private readonly RabbitMQService rabbitMQService;
        public AgentRL(EInsuranceDbContext context,EmailService emailService,RabbitMQService rabbitMQService)
        {
            this._context = context;
            this._emailService = emailService;
            this.rabbitMQService = rabbitMQService;
        }
        public async Task<InsuranceAgentEntity> CreateAgentAsync(InsuranceAgentEntity insuranceAgentML)
        {    
            _context.InsuranceAgents.AddAsync(insuranceAgentML);
            await _context.SaveChangesAsync();
            EmailML emailML = new EmailML
            {
                Name = insuranceAgentML.Username,
                Email = insuranceAgentML.Email,
                Password = PasswordHashing.Decrypt(insuranceAgentML.Password),
            };
            _emailService.SendRegisterMail(emailML);
            rabbitMQService.SendProductMessage(insuranceAgentML);
            return insuranceAgentML;
        }
    }
}
