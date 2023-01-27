using FootballWesbshop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootballWesbshop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SiteUser> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shoe>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Shoe>()
                .HasOne(t => t.Brand)
                .WithMany()
                .HasForeignKey(t => t.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<Brand>().HasData(new Brand() { Name = "Adidas", Uid = "1" });
            builder.Entity<Brand>().HasData(new Brand() { Name = "Nike", Uid = "2" });

            base.OnModelCreating(builder);
        }
    }
}