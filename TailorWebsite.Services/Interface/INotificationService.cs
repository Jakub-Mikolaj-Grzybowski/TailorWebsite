using System.Collections.Generic;
using System.Threading.Tasks;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Interface
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationViewModel>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task AddNotificationAsync(int userId, string message);
    }
}
