using System;
using Microsoft.AspNetCore.Identity;

namespace TailorWebsite.Model.DataModels;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    public User() { }

    // Navigation property for related orders
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    // Navigation property for related service reviews
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; } =
        new List<ServiceReview>();

    // Navigation property for related sizes
    public virtual ICollection<Size> Sizes { get; set; } = new List<Size>();

}
