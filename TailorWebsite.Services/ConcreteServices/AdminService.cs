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
    }
}
