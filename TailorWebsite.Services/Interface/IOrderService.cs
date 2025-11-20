using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;
 

public interface IOrderService
{
    Task<OrderCreateViewModel> PlaceOrderAsync(OrderCreateViewModel vm, int userId);
    Task<OrderCreateViewModel?> GetByIdAsync(int id);
    Task<IEnumerable<OrderCreateViewModel>> GetByUserAsync(int userId);
    Task<bool> CancelOrderAsync(int id);
}
