using LocalServicesDirectory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure
{
    public class LocalServicesContext : DbContext
    {
        public LocalServicesContext(DbContextOptions<LocalServicesContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Rating> Ratings => Set<Rating>();
    }
}
