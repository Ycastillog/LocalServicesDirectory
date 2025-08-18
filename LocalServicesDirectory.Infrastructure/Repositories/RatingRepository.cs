using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Repositories
{
    public class RatingRepository : BaseRepository<Rating>
    {
        public RatingRepository(LocalServicesContext ctx) : base(ctx) { }

        public async Task<List<Rating>> GetByServiceAsync(Guid serviceId, CancellationToken ct = default)
        {
            return await _ctx.Ratings
                .Where(r => r.ServiceId == serviceId)
                .ToListAsync(ct);
        }
    }
}

