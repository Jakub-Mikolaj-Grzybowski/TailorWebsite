using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers
{
    [Authorize(Roles = "Tailor")]
    public class TailorController : Controller
    {
        private readonly ITailorService _tailorService;

        public TailorController(ITailorService tailorService)
        {
            _tailorService = tailorService;
        }

        public async Task<IActionResult> ManageOrders()
        {
            var orders = await _tailorService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            var order = await _tailorService.GetOrderByIdAsync(id);
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
            var success = await _tailorService.UpdateOrderAsync(model);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("ManageOrders");
        }
    }
}
