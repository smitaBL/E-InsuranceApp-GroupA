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
    public class AdminRL : IAdminRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly EmailService emailService;

        public AdminRL(EInsuranceDbContext context, EmailService emailService)
        {
            _context = context;
            this.emailService = emailService;
        }

        public async Task<AdminEntity> RegisterAsync(AdminEntity admin)
        {
            try
            {
                var adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.AdminID == admin.AdminID);
                if (adminEntity != null)
                    throw new AdminException("Email Id Already Exists Please Login!!");
                _context.Admins.Add(adminEntity);
                EmailML emailML = new EmailML()
                {
                    Email = adminEntity.Email,
                    Password = adminEntity.Password,
                };
                emailService.SendRegisterMail(emailML);
                await _context.SaveChangesAsync();
                return adminEntity;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
