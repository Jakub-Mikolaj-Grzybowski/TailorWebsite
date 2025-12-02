using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.ConcreteServices;

public class OrderService : BaseService, IOrderService
{
    public OrderService(
        ApplicationDbContext dbContext,
        IMapper mapper,
        ILogger<OrderService> logger
    )
        : base(dbContext, mapper, logger) { }

    public async Task<OrderCreateViewModel> PlaceOrderAsync(OrderCreateViewModel vm, int userId)
    {
        var order = Mapper.Map<Order>(vm);
        order.UserId = userId;
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();
        Logger.LogInformation(
            "Placed Order {OrderId} for User {UserId} (Service {ServiceId})",
            order.Id,
            order.UserId,
            order.ServiceId
        );
        return Mapper.Map<OrderCreateViewModel>(order);
    }

    public async Task<OrderCreateViewModel?> GetByIdAsync(int id)
    {
        var order = await DbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return order == null ? null : Mapper.Map<OrderCreateViewModel>(order);
    }

    public async Task<IEnumerable<OrderCreateViewModel>> GetByUserAsync(int userId)
    {
        var orders = await DbContext
            .Orders.Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
        return orders.Select(o => Mapper.Map<OrderCreateViewModel>(o)).ToList();
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

    public async Task<bool> SetUserPickupDateAsync(int orderId, DateTime userPickupDate)
    {
        var order = await DbContext.Orders.FindAsync(orderId);
        if (order == null)
            return false;
        order.UserPickupDate = userPickupDate;
        await DbContext.SaveChangesAsync();
        Logger.LogInformation(
            "User set pickup date {PickupDate} for Order {OrderId}",
            userPickupDate,
            orderId
        );
        return true;
    }

    public async Task<List<ServiceViewModel>> GetTopServicesAsync(int count)
    {
        var topServices = await DbContext
            .Orders.Include(o => o.Service)
            .GroupBy(o => new { o.ServiceId, o.Service.Name })
            .Select(g => new ServiceViewModel
            {
                ServiceId = g.Key.ServiceId,
                Name = g.Key.Name,
                TotalOrders = g.Count(),
            })
            .OrderByDescending(s => s.TotalOrders)
            .ThenBy(s => s.Name)
            .Take(count)
            .ToListAsync();

        return topServices;
    }
}
