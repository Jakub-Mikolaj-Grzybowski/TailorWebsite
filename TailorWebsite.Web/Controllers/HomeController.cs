using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IReviewService _reviewService;
    private readonly IOrderService _orderService;
    private const int HomePageReviewCount = 3;
    private const int HomePageServiceCount = 3;

    public HomeController(
        ILogger<HomeController> logger,
        IReviewService reviewService,
        IOrderService orderService
    )
    {
        _logger = logger;
        _reviewService = reviewService;
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var reviews = await _reviewService.GetLatestReviewsAsync(HomePageReviewCount);
        var services = await _orderService.GetTopServicesAsync(HomePageServiceCount);
        var model = new HomePageViewModel { Reviews = reviews, PopularServices = services };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
