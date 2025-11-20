using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;

namespace TailorWebsite.Services.ConcreteServices;

public class SizeService : BaseService, ISizeService
{
    public SizeService(ApplicationDbContext dbContext, IMapper mapper, ILogger<SizeService> logger)
        : base(dbContext, mapper, logger) { }

    public async Task<Size> CreateAsync(Size size)
    {
        DbContext.Sizes.Add(size);
        await DbContext.SaveChangesAsync();
        Logger.LogInformation("Created Size {SizeId} for User {UserId}", size.Id, size.UserId);
        return size;
    }

    public async Task<Size?> GetByIdAsync(int id)
    {
        return await DbContext.Sizes.FindAsync(id);
    }

    public async Task<IEnumerable<Size>> GetByUserAsync(int userId)
    {
        return await DbContext
            .Sizes.Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await DbContext.Sizes.FindAsync(id);
        if (entity == null)
            return false;
        DbContext.Sizes.Remove(entity);
        await DbContext.SaveChangesAsync();
        Logger.LogInformation("Deleted Size {SizeId}", id);
        return true;
    }
}
