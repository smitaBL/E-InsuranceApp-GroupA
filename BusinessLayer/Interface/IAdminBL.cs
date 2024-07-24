using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        Task DeleteAdminByIdAsync(int id);
        Task<AdminEntity> GetAdminByIdAsync(int id);
        Task<List<AdminEntity>> GetAllAdminAsync();
        Task RegisterAsync(AdminML model);
        Task UpdateAdminByIdAsync(int id, AdminML model);
    }
}
