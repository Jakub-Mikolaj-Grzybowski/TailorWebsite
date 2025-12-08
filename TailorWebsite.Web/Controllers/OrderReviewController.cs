using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers
{
    [Authorize]
    public class OrderReviewController : Controller
    {
        private readonly IOrderReviewService _orderReviewService;

        public OrderReviewController(IOrderReviewService orderReviewService)
        {
            _orderReviewService = orderReviewService;
        }

        [HttpGet]
        public IActionResult Create(int orderId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var model = new OrderReviewViewModel { OrderId = orderId, UserId = userId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _orderReviewService.AddReviewAsync(model);
                return RedirectToAction("List", "Orders");
            }
            return View(model);
        }
    }
}
