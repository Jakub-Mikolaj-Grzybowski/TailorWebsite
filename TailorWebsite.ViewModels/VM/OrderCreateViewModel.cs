using System;
using System.ComponentModel.DataAnnotations;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.ViewModels.VM
{
    public class OrderCreateViewModel
    {
        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due date")]
        public DateTime? OrderDueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Order date")]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;

        [DataType(DataType.Currency)]
        [Display(Name = "Cena")]
        public decimal? TotalPrice { get; set; }
        public ServiceReview? ServiceReview { get; set; }
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public Service? Service { get; set; }
        public User? User { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data odbioru (wybrana przez użytkownika)")]
        public DateTime? UserPickupDate { get; set; }
    }
}
