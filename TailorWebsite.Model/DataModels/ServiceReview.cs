namespace TailorWebsite.Model.DataModels;

public class ServiceReview
{
    public int Id { get; set; }
    public int Rating { get; set; } // e.g., 1 to 5
    public string Comment { get; set; } = null!;
    public DateTime ReviewDate { get; set; } = DateTime.Now;

    // Foreign key to Customer
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    // Foreign key to Service
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;

    public ServiceReview() { }
}
