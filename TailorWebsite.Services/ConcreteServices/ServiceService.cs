using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;

namespace TailorWebsite.Services.ConcreteServices
{
    public class ServiceService : BaseService, IServiceService
    {
        public ServiceService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            ILogger<ServiceService> logger
        )
            : base(dbContext, mapper, logger) { }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await DbContext.Services.AsNoTracking().ToListAsync();
        }

        public Dictionary<string, List<Service>> GroupServicesByCategory(
            IEnumerable<Service> services
        )
        {
            return services
                .GroupBy(s => GetCategory(s.Name))
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        private string GetCategory(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return "Pozostałe";

            string category = serviceName;

            var separators = new[] { " - ", ": ", " – " };
            foreach (var sep in separators)
            {
                if (serviceName.Contains(sep))
                {
                    category = serviceName.Substring(0, serviceName.IndexOf(sep)).Trim();
                    break;
                }
            }

            if (category == serviceName)
            {
                var parts = serviceName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    category = parts[0];
                }
            }

            // Remove square brackets if present
            category = category.Replace("[", "").Replace("]", "").Trim();

            return category;
        }
    }
}
