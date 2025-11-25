using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.Model.DataModels;

namespace TailorWebsite.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    // table properties
    public DbSet<Order> Orders { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<ServiceReview> ServiceReviews { get; set; }

        public DbSet<Notification> Notifications { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //configuration commands
        optionsBuilder.UseLazyLoadingProxies(); //enable lazy loading proxies
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Fluent API commands
        modelBuilder
            .Entity<Order>()
            .HasOne(s => s.Service)
            .WithMany(o => o.Orders)
            .HasForeignKey(s => s.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Order>()
            .HasOne(u => u.User)
            .WithMany(o => o.Orders)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<ServiceReview>()
            .HasOne(u => u.User)
            .WithMany(sr => sr.ServiceReviews)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Size>()
            .HasOne(u => u.User)
            .WithMany(s => s.Sizes)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.ServiceReview)
            .WithOne(sr => sr.Order)
            .HasForeignKey<ServiceReview>(sr => sr.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
