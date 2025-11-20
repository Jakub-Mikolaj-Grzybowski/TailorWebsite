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

using TailorWebsite.ViewModels.VM;

public class SizeService : BaseService, ISizeService
{
    public SizeService(ApplicationDbContext dbContext, IMapper mapper, ILogger<SizeService> logger)
        : base(dbContext, mapper, logger) { }

    public async Task<SizeCreateViewModel> CreateAsync(Size size)
    {
        DbContext.Sizes.Add(size);
        await DbContext.SaveChangesAsync();
        Logger.LogInformation("Created Size {SizeId} for User {UserId}", size.Id, size.UserId);
        return Mapper.Map<SizeCreateViewModel>(size);
    }

    public async Task<SizeCreateViewModel?> GetByIdAsync(int id)
    {
        var size = await DbContext.Sizes.FindAsync(id);
        return size == null ? null : Mapper.Map<SizeCreateViewModel>(size);
    }

    public async Task<IEnumerable<SizeCreateViewModel>> GetByUserAsync(int userId)
    {
        var sizes = await DbContext
            .Sizes.Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
        return sizes.Select(s => Mapper.Map<SizeCreateViewModel>(s));
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
