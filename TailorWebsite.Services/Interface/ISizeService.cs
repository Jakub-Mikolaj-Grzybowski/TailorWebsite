using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.Services.Interface;

public interface ISizeService
{
    /// <summary>
    /// Create and persist a new Size entry.
    /// </summary>
    Task<Size> CreateAsync(Size size);

    /// <summary>
    /// Get a single Size by id.
    /// </summary>
    Task<Size?> GetByIdAsync(int id);

    /// <summary>
    /// Get all Size entries for a specific user.
    /// </summary>
    Task<IEnumerable<Size>> GetByUserAsync(int userId);

    /// <summary>
    /// Delete a Size by id. Returns true if deleted.
    /// </summary>
    Task<bool> DeleteAsync(int id);
}
