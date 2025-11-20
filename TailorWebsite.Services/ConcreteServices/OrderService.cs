using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;

namespace TailorWebsite.Services.ConcreteServices;

public class OrderService : BaseService, IOrderService
{
    public OrderService(
        ApplicationDbContext dbContext,
        IMapper mapper,
        ILogger<OrderService> logger
    )
        : base(dbContext, mapper, logger) { }

    public async Task<Order> PlaceOrderAsync(Order order)
    {
        // Basic defaults/enforcement
        if (string.IsNullOrWhiteSpace(order.Status))
            order.Status = "Pending";
        
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();
        Logger.LogInformation(
            "Placed Order {OrderId} for User {UserId} (Service {ServiceId})",
            order.Id,
            order.UserId,
            order.ServiceId
        );
        return order;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        // Lazy loading is enabled, but keep a sample Include for clarity if needed later
        return await DbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetByUserAsync(int userId)
    {
        return await DbContext
            .Orders.Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<bool> CancelOrderAsync(int id)
    {
        var entity = await DbContext.Orders.FindAsync(id);
        if (entity == null)
            return false;

        DbContext.Orders.Remove(entity);
        await DbContext.SaveChangesAsync();
        Logger.LogInformation("Cancelled Order {OrderId}", id);
        return true;
    }
}
