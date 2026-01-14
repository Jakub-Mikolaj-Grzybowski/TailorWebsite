namespace TailorWebsite.Model.DataModels;

public class ServiceReview
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime ReviewDate { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;

    public ServiceReview() { }
}
