using Microsoft.EntityFrameworkCore;
using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Infrastructure.Persistence
{
    public class LocalServicesContext : DbContext
    {
        public LocalServicesContext(DbContextOptions<LocalServicesContext> options) : base(options) { }

        public DbSet<Service> Services => Set<Service>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>(e =>
            {
                e.Property(x => x.Latitude).HasPrecision(9, 6);
                e.Property(x => x.Longitude).HasPrecision(9, 6);
            });
        }
    }
}
