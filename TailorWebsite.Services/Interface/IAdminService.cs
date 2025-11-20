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
        Task<List<OrderCreateViewModel>> GetAllOrdersAsync();
        Task<OrderCreateViewModel?> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderAsync(OrderCreateViewModel model);
    }
}
