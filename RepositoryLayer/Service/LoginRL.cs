using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Interface;
using RepositoryLayer.Utilities;
using System;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class LoginRL : ILoginRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginRL> _logger;
        private string _decryptPassword;

        public LoginRL(EInsuranceDbContext context, IConfiguration configuration, ILogger<LoginRL> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> LoginAsync(LoginML model)
        {
            try
            {
                _logger.LogInformation("Attempting login for role: {Role} with email: {Email}", model.Role, model.Email);

                switch (model.Role)
                {
                    case "Admin":
                        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());

                        if (admin == null)
                        {
                            _logger.LogWarning("Invalid email/password for admin with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                        _decryptPassword = PasswordHashing.Decrypt(admin.Password);

                        if (_decryptPassword.Equals(model.Password))
                        {
                            _logger.LogInformation("Admin login successful for email: {Email}", model.Email);
                            return JwtTokenGenerator.GenerateToken(_context, _configuration, admin, admin.AdminID, model.Role);
                        }
                        else
                        {
                            _logger.LogWarning("Invalid password for admin with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                    case "Employee":
                        var employee = await _context.Employees.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());

                        if (employee == null)
                        {
                            _logger.LogWarning("Invalid email/password for employee with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                        _decryptPassword = PasswordHashing.Decrypt(employee.Password);

                        if (_decryptPassword.Equals(model.Password))
                        {
                            _logger.LogInformation("Employee login successful for email: {Email}", model.Email);
                            return JwtTokenGenerator.GenerateToken(_context, _configuration, employee, employee.EmployeeID, model.Role);
                        }
                        else
                        {
                            _logger.LogWarning("Invalid password for employee with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                    case "Agent":
                        var agent = await _context.InsuranceAgents.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());

                        if (agent == null)
                        {
                            _logger.LogWarning("Invalid email/password for agent with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                        _decryptPassword = PasswordHashing.Decrypt(agent.Password);

                        if (_decryptPassword.Equals(model.Password))
                        {
                            _logger.LogInformation("Agent login successful for email: {Email}", model.Email);
                            return JwtTokenGenerator.GenerateToken(_context, _configuration, agent, agent.AgentID, model.Role);
                        }
                        else
                        {
                            _logger.LogWarning("Invalid password for agent with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                    case "Customer":
                        var customer = await _context.Customers.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());

                        if (customer == null)
                        {
                            _logger.LogWarning("Invalid email/password for customer with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                        _decryptPassword = PasswordHashing.Decrypt(customer.Password);

                        if (_decryptPassword.Equals(model.Password))
                        {
                            _logger.LogInformation("Customer login successful for email: {Email}", model.Email);
                            return JwtTokenGenerator.GenerateToken(_context, _configuration, customer, customer.CustomerID, model.Role);
                        }
                        else
                        {
                            _logger.LogWarning("Invalid password for customer with email: {Email}", model.Email);
                            throw new LoginException("Invalid Email/Password");
                        }

                    default:
                        _logger.LogWarning("Invalid role provided for login: {Role}", model.Role);
                        throw new LoginException("Invalid Login Credentials");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during login for email: {Email}", model.Email);
                throw new LoginException(ex.Message);
            }
        }
    }
}
