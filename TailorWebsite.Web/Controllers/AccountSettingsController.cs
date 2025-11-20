using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers;

[Authorize]
public class AccountSettingsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IAccountSettingsService _accountSettingsService;

    public AccountSettingsController(
        UserManager<User> userManager,
        IAccountSettingsService accountSettingsService
    )
    {
        _userManager = userManager;
        _accountSettingsService = accountSettingsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Index", "Home");

        var result = await _accountSettingsService.ChangePasswordAsync(
            user,
            vm.OldPassword,
            vm.NewPassword
        );
        if (result.Succeeded)
        {
            TempData["Success"] = "Hasło zostało zmienione.";
            return RedirectToAction("Index");
        }
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
        return View(vm);
    }

    [HttpGet]
    public IActionResult AddPhoneNumber()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Index", "Home");

        var result = await _accountSettingsService.SetPhoneNumberAsync(user, vm.PhoneNumber);
        if (result.Succeeded)
        {
            TempData["Success"] = "Numer telefonu został dodany.";
            return RedirectToAction("Index");
        }
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAccount()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Index", "Home");

        var result = await _accountSettingsService.DeleteAccountAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        TempData["Error"] = "Nie udało się usunąć konta.";
        return RedirectToAction("Index");
    }
}
