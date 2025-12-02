using System.Collections.Generic;

namespace TailorWebsite.ViewModels.VM
{
    public class HomePageViewModel
    {
        public List<ReviewViewModel> Reviews { get; set; } = new();
        public List<ServiceViewModel> PopularServices { get; set; } = new();
    }
}
