using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface;

public interface IOrderService
{
    Task<OrderCreateViewModel> PlaceOrderAsync(OrderCreateViewModel vm, int userId);
    Task<OrderCreateViewModel?> GetByIdAsync(int id);
    Task<IEnumerable<OrderCreateViewModel>> GetByUserAsync(int userId);
    Task<bool> CancelOrderAsync(int id);

    // Ustawienie daty odbioru przez użytkownika
    Task<bool> SetUserPickupDateAsync(int orderId, DateTime userPickupDate);

    Task<List<ServiceViewModel>> GetTopServicesAsync(int count);
}
