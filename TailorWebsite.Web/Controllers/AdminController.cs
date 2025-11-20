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

        public async Task<IActionResult> Index()
        {
            var userViewModels = await _adminService.GetAllUsersAsync();
            return View(userViewModels);
        }

        public async Task<IActionResult> ManageOrders()
        {
            var orders = await _adminService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            var order = await _adminService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(OrderCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _adminService.UpdateOrderAsync(model);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("ManageOrders");
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
