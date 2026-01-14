using System.ComponentModel.DataAnnotations.Schema;

namespace TailorWebsite.Model.DataModels;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? OrderDueDate { get; set; } 
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; } 

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
    public virtual ServiceReview ServiceReview { get; set; } = null!;

    public DateTime? UserPickupDate { get; set; } 

    public Order() { }
}
