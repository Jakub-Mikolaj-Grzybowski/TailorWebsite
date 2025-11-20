using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface;


public interface ISizeService
{
    Task<SizeCreateViewModel> CreateAsync(Size size);

    Task<SizeCreateViewModel?> GetByIdAsync(int id);
    Task<IEnumerable<SizeCreateViewModel>> GetByUserAsync(int userId);

    Task<bool> DeleteAsync(int id);
}
