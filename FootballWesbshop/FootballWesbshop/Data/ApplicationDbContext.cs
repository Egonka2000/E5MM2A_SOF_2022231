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
                .HasMany<SiteUser>(t => t.Owners)
                .WithMany(t => t.Cart);
                

            builder.Entity<Shoe>()
                .HasOne(t => t.Brand)
                .WithMany()
                .HasForeignKey(t => t.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<Brand>().HasData(new Brand() { Name = "Adidas", Uid = "1", Type = BrandType.Predator });
            builder.Entity<Brand>().HasData(new Brand() { Name = "Adidas", Uid = "2", Type = BrandType.X });
            builder.Entity<Brand>().HasData(new Brand() { Name = "Adidas", Uid = "3", Type = BrandType.Nemesis });
            builder.Entity<Brand>().HasData(new Brand() { Name = "Nike", Uid = "4", Type = BrandType.Mercurial });
            builder.Entity<Brand>().HasData(new Brand() { Name = "Nike", Uid = "5", Type = BrandType.Hypervenom });

            base.OnModelCreating(builder);
        }
    }
}