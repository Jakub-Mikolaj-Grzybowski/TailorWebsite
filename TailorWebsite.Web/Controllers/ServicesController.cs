using Microsoft.AspNetCore.Mvc;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;

namespace TailorWebsite.Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IOrderService _orderService;

        public ServicesController(IServiceService serviceService, IOrderService orderService)
        {
            _serviceService = serviceService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllServicesAsync();
            var topServicesVM = await _orderService.GetTopServicesAsync(6);

            var popularServices = new List<Service>();
            foreach (var top in topServicesVM)
            {
                var service = services.FirstOrDefault(s => s.Id == top.ServiceId);
                if (service != null)
                {
                    popularServices.Add(service);
                }
            }

            var groupedServices = _serviceService.GroupServicesByCategory(services);

            var finalGrouping = new Dictionary<string, List<Service>>();

            if (popularServices.Any())
            {
                finalGrouping.Add("Popularne usługi", popularServices);
            }

            foreach (var kvp in groupedServices)
            {
                finalGrouping.Add(kvp.Key, kvp.Value);
            }

            return View(finalGrouping);
        }
    }
}
