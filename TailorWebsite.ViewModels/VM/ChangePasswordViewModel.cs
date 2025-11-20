using System.ComponentModel.DataAnnotations;

namespace TailorWebsite.ViewModels.VM;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Stare hasło")]
    public string OldPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Nowe hasło")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Potwierdź nowe hasło")]
    [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same.")]
    public string ConfirmPassword { get; set; }
}
