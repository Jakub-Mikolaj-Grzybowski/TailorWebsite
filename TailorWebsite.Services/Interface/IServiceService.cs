using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.Services.Interface
{
    public interface IServiceService
    {
        Task<List<Service>> GetAllServicesAsync();
        Dictionary<string, List<Service>> GroupServicesByCategory(IEnumerable<Service> services);
    }
}
