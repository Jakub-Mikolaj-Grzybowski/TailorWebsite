using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.ConcreteServices
{
    public class TailorService : ITailorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public TailorService(
            ApplicationDbContext context,
            IMapper mapper,
            INotificationService notificationService
        )
        {
            _context = context;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<List<OrderCreateViewModel>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.Include(o => o.User).ToListAsync();
            return orders.Select(o => _mapper.Map<OrderCreateViewModel>(o)).ToList();
        }

        public async Task<OrderCreateViewModel?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order == null ? null : _mapper.Map<OrderCreateViewModel>(order);
        }

        public async Task<bool> UpdateOrderAsync(OrderCreateViewModel model)
        {
            var order = await _context
                .Orders.Include(s => s.Service)
                .FirstOrDefaultAsync(o => o.Id == model.Id);
            if (order == null)
                return false;
            
            order.Status = model.Status;
            order.OrderDueDate = model.OrderDueDate;
            await _context.SaveChangesAsync();

            if (order.Status == OrderStatus.Completed)
            {
                await _notificationService.AddNotificationAsync(
                    order.UserId,
                    $"Twoje zamówienie {order.Service?.Name} zostało ukończone."
                );
            }
            return true;
        }
    }
}
