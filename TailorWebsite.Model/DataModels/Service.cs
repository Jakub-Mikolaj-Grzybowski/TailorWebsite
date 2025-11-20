namespace TailorWebsite.Model.DataModels;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string AdditionalMaterials { get; set; } = null!;


    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public Service() { }
}