using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Repositories
{
    public class ServiceRepository : BaseRepository<Service>
    {
        public ServiceRepository(LocalServicesContext ctx) : base(ctx) { }

        public async Task<List<Service>> GetWithCategoryCityAsync(CancellationToken ct = default)
        {
            return await _ctx.Services
                .Include(s => s.Category)
                .Include(s => s.City)
                .ToListAsync(ct);
        }
    }
}

