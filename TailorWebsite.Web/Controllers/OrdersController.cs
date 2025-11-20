using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, ApplicationDbContext db, IMapper mapper)
    {
        _orderService = orderService;
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var order = await _orderService.GetByIdAsync(id);
        if (order == null || order.User == null || order.User.Id != userId)
            return NotFound();
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(int id, DateTime? userPickupDate)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var order = await _orderService.GetByIdAsync(id);
        if (order == null || order.User == null || order.User.Id != userId)
            return NotFound();

        // Pozwól ustawić datę odbioru tylko jeśli zamówienie jest zakończone
        if (order.Status == OrderStatus.Completed && userPickupDate.HasValue)
        {
            await _orderService.SetUserPickupDateAsync(id, userPickupDate.Value);
            TempData["Success"] = "Data odbioru została ustawiona.";
            return RedirectToAction(nameof(List));
        }
        TempData["Error"] = "Nie można ustawić daty odbioru dla tego zamówienia.";
        return View(order);
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

        var serviceEntity = await _db
            .Services.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == vm.ServiceId);
        if (serviceEntity != null)
        {
            vm.TotalPrice = serviceEntity.Price * vm.Quantity;
        }
        await _orderService.PlaceOrderAsync(vm, userId);
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var domainOrder = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (domainOrder == null)
            return NotFound();

        // Only allow cancelling pending orders
        if (domainOrder.Status != OrderStatus.Pending)
        {
            TempData["Error"] = "Tego zamówienia nie można anulować.";
            return RedirectToAction(nameof(List));
        }

        await _orderService.CancelOrderAsync(id);

        TempData["Success"] = "Zamówienie anulowane.";
        return RedirectToAction(nameof(List));
    }
}
