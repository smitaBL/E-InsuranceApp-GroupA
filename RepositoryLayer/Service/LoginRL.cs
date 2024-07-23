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
        public LoginRL(EInsuranceDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginML model)
        {
            object? user = null;

            user = model.Role switch
            {
                "Admin" => await _context.Admins.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower()),
                "Employee" => await _context.Employees.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower()),
                "Agent" => await _context.InsuranceAgents.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower()),
                "Customer" => await _context.Customers.FirstOrDefaultAsync(a => a.Email.ToLower() == model.Email.ToLower()),
                _ => throw new LoginException("Invalid Login Credentials"),
            };
            if (user == null)
            {
                throw new LoginException("Invalid Email/Password");
            }

            string decryptPassword = PasswordHashing.Decrypt(model.Password);

            if (decryptPassword.Equals(model.Password))
            {
                return JwtTokenGenerator.GenerateToken(_context,_configuration,user,model.Role);
            }
            else
            {
                throw new LoginException("Invalid Email/Password");
            }
        }

    }
}
