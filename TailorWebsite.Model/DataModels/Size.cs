namespace TailorWebsite.Model.DataModels;

public class Size
{
    public int Id { get; set; }
    public int ChestCircumference { get; set; }
    public int WaistCircumference { get; set; } 
    public int HipCircumference { get; set; } 
    public int SleeveLength { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public Size() { }
}
