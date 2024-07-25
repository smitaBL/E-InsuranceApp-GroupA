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
    public class AdminRL : IAdminRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService rabbitMQService;

        public AdminRL(EInsuranceDbContext context, RabbitMQService rabbitMQService)
        {
            _context = context;
            this.rabbitMQService = rabbitMQService;
        }

        public async Task DeleteAdminByIdAsync(int id)
        {
            try
            {
                var admin = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteAdminById @Id = {0}", id);

                if (admin == null)
                {
                    throw new AdminException("Admin not found");
                }
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }

        public async Task<AdminEntity> GetAdminByIdAsync(int id)
        {
            try
            {
                var admins = await _context.Admins
                .FromSqlRaw("EXEC sp_GetAdminById @Id = {0}", id)
                .ToListAsync();

                var admin = admins.FirstOrDefault();

                if (admin == null)
                {
                    throw new AdminException("Admin not found");
                }
                return admin;
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }

        public async Task<List<AdminEntity>> GetAllAdminAsync()
        {
            try
            {
                return await _context.Admins.FromSqlRaw("EXEC sp_GetAllAdmins").ToListAsync();
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }

        public async Task RegisterAsync(AdminEntity admin)
        {
            try
            {
                var adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.AdminID == admin.AdminID);
                if (adminEntity != null)
                    throw new AdminException("Email Id Already Exists Please Login!!");
                _context.Admins.Add(admin);

                EmailML emailML = new EmailML()
                {
                    Name = admin.FullName,
                    Email = admin.Email,
                    Password = admin.Password,
                };
                rabbitMQService.SendProductMessage(emailML);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }

        public async Task UpdateAdminAsync(int id, AdminEntity admin)
        {
            try
            {
                var admins = await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateAdminById @Id = {0}, @Username = {1}, @Password = {2}, @Email = {3}, @FullName = {4}",
                id, admin.Username, admin.Password, admin.Email, admin.FullName);

                if (admins == 0)
                {
                    throw new AdminException("Admin not found or could not be updated");
                }


                EmailML emailML = new EmailML()
                {
                    Name = admin.FullName,
                    Email = admin.Email,
                    Password = admin.Password,
                };
                rabbitMQService.SendProductMessage(emailML);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AdminException(ex.Message);
            }
        }
    }
}
