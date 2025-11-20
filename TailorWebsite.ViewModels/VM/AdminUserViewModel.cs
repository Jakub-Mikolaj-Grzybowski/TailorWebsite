namespace TailorWebsite.ViewModels.VM
{
    public class AdminUserViewModel
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
