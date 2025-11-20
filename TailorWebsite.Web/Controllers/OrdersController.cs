using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ApplicationDbContext _db;

    public OrdersController(IOrderService orderService, ApplicationDbContext db)
    {
        _orderService = orderService;
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var orders = await _orderService.GetByUserAsync(userId);
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var services = await _db.Services.AsNoTracking().ToListAsync();
        ViewBag.Services = services
            .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = $"{s.Name}" })
            .ToList();
        ViewBag.ServicesData = services.Select(s => new { id = s.Id, price = s.Price });
        var vm = new OrderCreateViewModel { Quantity = 1, OrderDate = DateTime.Today };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderCreateViewModel vm)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        if (!ModelState.IsValid)
        {
            var servicesAll = await _db.Services.AsNoTracking().ToListAsync();
            ViewBag.Services = servicesAll
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = $"{s.Name}" })
                .ToList();
            ViewBag.ServicesData = servicesAll.Select(s => new { id = s.Id, price = s.Price });
            return View(vm);
        }

        var order = new Order
        {
            ServiceId = vm.ServiceId,
            Quantity = vm.Quantity,
            UserId = userId,
            Status = "Pending",
            OrderDate = DateTime.Now,
            TotalPrice = 0m
        };

        // Oblicz cenę całkowitą na podstawie aktualnej ceny usługi
        var serviceEntity = await _db.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == vm.ServiceId);
        if (serviceEntity != null)
        {
            order.TotalPrice = serviceEntity.Price * order.Quantity;
        }

        await _orderService.PlaceOrderAsync(order);
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var order = await _orderService.GetByIdAsync(id);
        if (order == null || order.UserId != userId)
            return NotFound();

        // (Optional) Only allow cancelling pending orders
        if (!string.Equals(order.Status, "Pending", StringComparison.OrdinalIgnoreCase))
        {
            TempData["Error"] = "Tego zamówienia nie można anulować.";
            return RedirectToAction(nameof(List));
        }

        await _orderService.CancelOrderAsync(id);
        TempData["Success"] = "Zamówienie anulowane.";
        return RedirectToAction(nameof(List));
    }
}
