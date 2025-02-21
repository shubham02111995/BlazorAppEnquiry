using Microsoft.EntityFrameworkCore;
using BlazorAppEnquiry.Models;

namespace BlazorAppEnquiry.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("5b8fbdd47c514c09bac4a50ab4c15364"),
                Username = "admin",
                Password = "gcvadmin@123",
                Role = Roles.Admin
            });
        }
    }
}
