using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.ConcreteServices
{
    public class AdminService : IAdminService
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(
            UserManager<User> userManager,
            ApplicationDbContext context,
            IMapper mapper,
            INotificationService notificationService
        )
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<List<AdminUserViewModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(
                    new AdminUserViewModel
                    {
                        UserId = user.Id.ToString(),
                        Email = user.Email,
                        Roles = roles,
                    }
                );
            }
            return userViewModels;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
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
                    $"Twoje zamówienie nr {order.Service.Name} zostało ukończone."
                );
            }
            return true;
        }
    }
}
