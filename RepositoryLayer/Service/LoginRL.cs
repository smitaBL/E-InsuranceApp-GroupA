using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Context;
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
    public class LoginRL : ILoginRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly IConfiguration _configuration;
        string decryptPassword;
        public LoginRL(EInsuranceDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginML model)
        {
            switch (model.Role)
            {
                case "Admin":
                    var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());

                    if (admin == null)
                    {
                        throw new LoginException("Invalid Email/Password");
                    }

                    decryptPassword = PasswordHashing.Decrypt(admin.Password);

                    if (decryptPassword.Equals(model.Password))
                    {
                        return JwtTokenGenerator.GenerateToken(_context, _configuration, admin,admin.AdminID, model.Role); //token;
                    }
                    else
                    {
                        throw new LoginException("Invalid Email/Password");
                    }
                    break;

                case "Employee":
                    var employee = await _context.Employees.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());
                    if (employee == null)
                    {
                        throw new LoginException("Invalid Email/Password");
                    }

                    decryptPassword = PasswordHashing.Decrypt(employee.Password);

                    if (decryptPassword.Equals(model.Password))
                    {
                        return JwtTokenGenerator.GenerateToken(_context, _configuration, employee,employee.EmployeeID, model.Role); //token;
                    }
                    else
                    {
                        throw new LoginException("Invalid Email/Password");
                    }
                    break;

                case "Agent":
                    var agent = await _context.InsuranceAgents.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());
                    if (agent == null)
                    {
                        throw new LoginException("Invalid Email/Password");
                    }

                    decryptPassword = PasswordHashing.Decrypt(agent.Password);

                    if (decryptPassword.Equals(model.Password))
                    {
                        return JwtTokenGenerator.GenerateToken(_context, _configuration, agent,agent.AgentID, model.Role); //token;
                    }
                    else
                    {
                        throw new LoginException("Invalid Email/Password");
                    }
                    break;

                case "Customer":
                    var customer = await _context.Customers.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower());
                    if (customer == null)
                    {
                        throw new LoginException("Invalid Email/Password");
                    }

                    decryptPassword = PasswordHashing.Decrypt(customer.Password);

                    if (decryptPassword.Equals(model.Password))
                    {
                        return JwtTokenGenerator.GenerateToken(_context, _configuration, customer,customer.CustomerID, model.Role); //token;
                    }
                    else
                    {
                        throw new LoginException("Invalid Email/Password");
                    }
                    break;

                default:
                    throw new LoginException("Invalid Login Credentials");
            }
        }
    }
}
