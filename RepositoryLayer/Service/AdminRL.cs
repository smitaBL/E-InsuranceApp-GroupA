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
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Service
{
    public class AdminRL : IAdminRL
    {
        private readonly EInsuranceDbContext _context;
        private readonly RabbitMQService _rabbitMQService;
        private readonly ILogger<AdminRL> _logger;

        public AdminRL(EInsuranceDbContext context, RabbitMQService rabbitMQService, ILogger<AdminRL> logger)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
            _logger = logger;
        }

        public async Task DeleteAdminByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting admin with ID: {id}", id);

                var admin = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteAdminById @Id = {0}", id);

                if (admin == null)
                {
                    _logger.LogWarning("Admin with ID: {id} not found", id);
                    throw new AdminException("Admin not found");
                }

                _logger.LogInformation("Admin with ID: {id} deleted successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting admin with ID: {id}", id);
                throw new AdminException(ex.Message);
            }
        }

        public async Task<AdminEntity> GetAdminByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching admin with ID: {id}", id);

                var admins = await _context.Admins
                                           .FromSqlRaw("EXEC sp_GetAdminById @Id = {0}", id)
                                           .ToListAsync();

                var admin = admins.FirstOrDefault();

                if (admin == null)
                {
                    _logger.LogWarning("Admin with ID: {id} not found", id);
                    throw new AdminException("Admin not found");
                }

                _logger.LogInformation("Fetched admin with ID: {id} successfully", id);
                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching admin with ID: {id}", id);
                throw new AdminException(ex.Message);
            }
        }

        public async Task<List<AdminEntity>> GetAllAdminAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all admins");

                var admins = await _context.Admins.FromSqlRaw("EXEC sp_GetAllAdmins").ToListAsync();

                _logger.LogInformation("Fetched all admins successfully");
                return admins;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching all admins");
                throw new AdminException(ex.Message);
            }
        }

        public async Task RegisterAsync(AdminEntity admin)
        {
            try
            {
                _logger.LogInformation("Registering admin with email: {email}", admin.Email);

                var adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.Email == admin.Email);
                if (adminEntity != null)
                {
                    _logger.LogWarning("Email ID: {email} already exists", admin.Email);
                    throw new AdminException("Email Id Already Exists Please Login!!");
                }

                _context.Admins.Add(admin);

                var emailML = new EmailML()
                {
                    Name = admin.FullName,
                    Email = admin.Email,
                    Password = admin.Password,
                };
                _rabbitMQService.SendProductMessage(emailML);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Registered admin with email: {email} successfully", admin.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while registering admin with email: {email}", admin.Email);
                throw new AdminException(ex.Message);
            }
        }

        public async Task UpdateAdminAsync(int id, AdminEntity admin)
        {
            try
            {
                _logger.LogInformation("Updating admin with ID: {id}", id);

                var admins = await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateAdminById @Id = {0}, @Username = {1}, @Password = {2}, @Email = {3}, @FullName = {4}",
                id, admin.Username, admin.Password, admin.Email, admin.FullName);

                if (admins == 0)
                {
                    _logger.LogWarning("Admin with ID: {id} not found or could not be updated", id);
                    throw new AdminException("Admin not found or could not be updated");
                }

                var emailML = new EmailML()
                {
                    Name = admin.FullName,
                    Email = admin.Email,
                    Password = admin.Password,
                };
                _rabbitMQService.SendProductMessage(emailML);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated admin with ID: {id} successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating admin with ID: {id}", id);
                throw new AdminException(ex.Message);
            }
        }
    }
}
