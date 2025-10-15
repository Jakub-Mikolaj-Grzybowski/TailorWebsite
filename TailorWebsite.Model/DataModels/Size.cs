namespace TailorWebsite.Model.DataModels;

public class Size
{
    public int Id { get; set; }
    public int ChestCircumference { get; set; } // in cm
    public int WaistCircumference { get; set; } // in cm
    public int HipCircumference { get; set; } // in cm
    public int SleeveLength { get; set; } // in cm
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Navigation property for Customer
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public Size(){}
}