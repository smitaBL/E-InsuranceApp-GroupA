using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        Task DeleteAdminByIdAsync(int id);
        Task<AdminEntity> GetAdminByIdAsync(int id);
        Task<List<AdminEntity>> GetAllAdminAsync();
        Task RegisterAsync(AdminEntity admin);
        Task UpdateAdminAsync(int id, AdminEntity admin);
    }
}
