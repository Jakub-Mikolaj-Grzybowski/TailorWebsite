using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewViewModel reviewViewModel);
        Task<List<ReviewViewModel>> GetLatestReviewsAsync(int count);
    }
}
