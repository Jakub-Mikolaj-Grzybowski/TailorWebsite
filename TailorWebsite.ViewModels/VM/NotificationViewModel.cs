using System;

namespace TailorWebsite.ViewModels.VM
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
