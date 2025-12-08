using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderService _orderService;
    private readonly ISizeService _sizeService;

    public ProfileController(
        UserManager<User> userManager,
        IOrderService orderService,
        ISizeService sizeService)
    {
        _userManager = userManager;
        _orderService = orderService;
        _sizeService = sizeService;
    }

    public async Task<IActionResult> Index()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userIdStr);
        var orders = await _orderService.GetByUserAsync(userId);
        var sizes = await _sizeService.GetByUserAsync(userId);

        var vm = new ProfileViewModel
        {
            UserId = userId,
            Name = user?.Name ?? user?.UserName ?? "",
            Email = user?.Email ?? "",
            PhoneNumber = user?.PhoneNumber ?? "",
            OrdersCount = orders.Count(),
            SizesCount = sizes.Count()
        };

        return View(vm);
    }

    public async Task<IActionResult> DeleteAccount()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userIdStr);
        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
           
            return BadRequest("Nie udało się usunąć konta.");
        }

     
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        return RedirectToAction("Index", "Home");
    }
}
