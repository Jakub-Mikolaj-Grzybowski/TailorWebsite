namespace TailorWebsite.ViewModels.VM;

public class ProfileViewModel
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
    public int SizesCount { get; set; }
}