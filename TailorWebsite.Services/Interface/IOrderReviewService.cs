using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface
{
    public interface IOrderReviewService
    {
        Task AddReviewAsync(OrderReviewViewModel reviewViewModel);
        Task<List<OrderReviewViewModel>> GetReviewsForOrderAsync(int orderId);
        Task<List<OrderReviewViewModel>> GetReviewsForUserAsync(int userId);
    }
}
