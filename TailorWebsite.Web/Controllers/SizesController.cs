using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers;

[Authorize]
public class SizesController : Controller
{
    private readonly ISizeService _sizeService;
    private readonly IMapper _mapper;

    public SizesController(ISizeService sizeService, IMapper mapper)
    {
        _sizeService = sizeService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new SizeCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SizeCreateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var size = _mapper.Map<Size>(vm);
        // user id from claim
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
        {
            return Forbid();
        }

        size.UserId = userId;
        await _sizeService.CreateAsync(size);
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
        {
            return Forbid();
        }
        var items = await _sizeService.GetByUserAsync(userId);
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _sizeService.DeleteAsync(id);
        return RedirectToAction(nameof(List));
    }
}
