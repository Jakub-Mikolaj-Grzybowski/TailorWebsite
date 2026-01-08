using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface
{
    public interface ITailorService
    {
        Task<List<OrderCreateViewModel>> GetAllOrdersAsync();
        Task<OrderCreateViewModel?> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderAsync(OrderCreateViewModel model);
    }
}
