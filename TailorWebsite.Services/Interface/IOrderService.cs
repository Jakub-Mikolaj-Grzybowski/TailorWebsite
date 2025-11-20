using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.Services.Interface;

public interface IOrderService
{
    /// <summary>
    /// Place a new order. Calculates derived values if needed and persists.
    /// </summary>
    Task<Order> PlaceOrderAsync(Order order);

    /// <summary>
    /// Get a single order by id (with related Service and User loaded lazily).
    /// </summary>
    Task<Order?> GetByIdAsync(int id);

    /// <summary>
    /// Get all orders for a specific user, newest first.
    /// </summary>
    Task<IEnumerable<Order>> GetByUserAsync(int userId);

    /// <summary>
    /// Cancel (delete) an order if allowed. Returns true if removed.
    /// </summary>
    Task<bool> CancelOrderAsync(int id);
}