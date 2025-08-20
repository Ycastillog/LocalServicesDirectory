using LocalServicesDirectory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Persistence
{
    public class LocalServicesContext : DbContext
    {
        public LocalServicesContext(DbContextOptions<LocalServicesContext> options) : base(options) { }

        public DbSet<Service> Services => Set<Service>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Rating> Ratings => Set<Rating>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(200).IsRequired();
                e.Property(p => p.Email).HasMaxLength(200);
                e.Property(p => p.Phone).HasMaxLength(50);
                e.Property(p => p.WebsiteUrl).HasMaxLength(300);

                e.HasOne(s => s.Category)
                    .WithMany(c => c.Services)
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(s => s.City)
                    .WithMany(c => c.Services)
                    .HasForeignKey(s => s.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Rating>(e =>
            {
                e.Property(r => r.Score).IsRequired();
                e.HasOne(r => r.Service)
                    .WithMany()
                    .HasForeignKey(r => r.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

