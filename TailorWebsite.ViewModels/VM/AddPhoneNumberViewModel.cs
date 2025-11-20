using System.ComponentModel.DataAnnotations;

namespace TailorWebsite.ViewModels.VM;

public class AddPhoneNumberViewModel
{
    [Required]
    [Phone]
    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; }
}
