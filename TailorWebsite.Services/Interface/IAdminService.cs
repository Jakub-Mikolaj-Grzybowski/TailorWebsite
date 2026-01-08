using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface
{
    public interface IAdminService
    {
        Task<List<AdminUserViewModel>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string userId);

    }
}
