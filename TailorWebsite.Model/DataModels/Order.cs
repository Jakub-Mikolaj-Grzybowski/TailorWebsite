using System.ComponentModel.DataAnnotations.Schema;

namespace TailorWebsite.Model.DataModels;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? OrderDueDate { get; set; } // null => Pending termin
    public string Status { get; set; } = null!; // e.g., "Pending", "In Progress", "Completed"
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; } 

    // Foreign key to Customer
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    //Navigation property for Service
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;

    

    public Order() { }
}
