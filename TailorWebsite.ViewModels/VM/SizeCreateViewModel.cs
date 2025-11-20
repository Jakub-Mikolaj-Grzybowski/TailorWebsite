using System.ComponentModel.DataAnnotations;

namespace TailorWebsite.ViewModels.VM;

public class SizeCreateViewModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Podaj obwód klatki piersiowej.")]
    [Range(30, 200, ErrorMessage = "Obwód klatki musi być w zakresie 30–200 cm.")]
    [Display(Name = "Obwód klatki piersiowej (cm)")]
    public int ChestCircumference { get; set; }

    [Required(ErrorMessage = "Podaj obwód talii.")]
    [Range(30, 200, ErrorMessage = "Obwód talii musi być w zakresie 30–200 cm.")]
    [Display(Name = "Obwód talii (cm)")]
    public int WaistCircumference { get; set; }

    [Required(ErrorMessage = "Podaj obwód bioder.")]
    [Range(30, 200, ErrorMessage = "Obwód bioder musi być w zakresie 30–200 cm.")]
    [Display(Name = "Obwód bioder (cm)")]
    public int HipCircumference { get; set; }

    [Required(ErrorMessage = "Podaj długość rękawa.")]
    [Range(30, 120, ErrorMessage = "Długość rękawa musi być w zakresie 30–120 cm.")]
    [Display(Name = "Długość rękawa (cm)")]
    public int SleeveLength { get; set; }
}
