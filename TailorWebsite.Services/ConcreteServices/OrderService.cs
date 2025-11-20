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
}
