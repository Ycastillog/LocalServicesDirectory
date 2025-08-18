using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(LocalServicesContext ctx)
        {
            if (ctx.Categories.Any()) return;

            var now = DateTime.UtcNow;

            var categories = new[]
            {
                new Category { Id = Guid.NewGuid(), Name = "Plomería",    Description = "Servicios de plomería", CreatedAt = now },
                new Category { Id = Guid.NewGuid(), Name = "Electricidad", Description = "Servicios eléctricos",  CreatedAt = now },
                new Category { Id = Guid.NewGuid(), Name = "Carpintería",  Description = "Trabajos en madera",    CreatedAt = now },
            };
            var cities = new[]
            {
                new City { Id = Guid.NewGuid(), Name = "Santo Domingo", CreatedAt = now },
                new City { Id = Guid.NewGuid(), Name = "Santiago",      CreatedAt = now },
                new City { Id = Guid.NewGuid(), Name = "La Romana",     CreatedAt = now },
            };

            ctx.Categories.AddRange(categories);
            ctx.Cities.AddRange(cities);
            ctx.SaveChanges();

            var svc1 = new Service
            {
                Id = Guid.NewGuid(),
                Name = "Plomería Pérez",
                Description = "Destapes, reparaciones y mantenimiento",
                Phone = "809-555-0001",
                Email = "contacto@plomeriaperez.do",
                Address = "Av. 27 de Febrero #123",
                CategoryId = categories[0].Id,
                CityId = cities[0].Id,
                Latitude = 18.486058m,
                Longitude = -69.931212m,
                WebsiteUrl = "https://plomeriaperez.do",
                IsVerified = true,
                CreatedAt = now
            };

            var svc2 = new Service
            {
                Id = Guid.NewGuid(),
                Name = "Electricidad Gómez",
                Description = "Instalaciones y mantenimiento eléctrico",
                Phone = "809-555-0002",
                Email = "soporte@electricidadgomez.do",
                Address = "Calle Duarte #45",
                CategoryId = categories[1].Id,
                CityId = cities[1].Id,
                Latitude = 19.451699m,
                Longitude = -70.694778m,
                WebsiteUrl = "https://electricidadgomez.do",
                IsVerified = false,
                CreatedAt = now
            };

            ctx.Services.AddRange(svc1, svc2);
            ctx.SaveChanges();
        }
    }
}
