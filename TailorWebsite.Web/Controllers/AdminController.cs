using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var userViewModels = await _adminService.GetAllUsersAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                userViewModels = userViewModels
                    .Where(u => u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(userViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var success = await _adminService.DeleteUserAsync(userId);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Nie udało się usunąć użytkownika.");
            return View(nameof(Index));
        }
    }
}
