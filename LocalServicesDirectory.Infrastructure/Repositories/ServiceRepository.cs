using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalServicesDirectory.Application.Interfaces.Repositories; // ✅
using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly LocalServicesContext _context;

        public ServiceRepository(LocalServicesContext context) => _context = context;

        public async Task<IEnumerable<Service>> GetAllAsync(CancellationToken ct = default)
            => await _context.Services.AsNoTracking().ToListAsync(ct);

        public async Task<Service?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);

        public async Task AddAsync(Service entity, CancellationToken ct = default)
        {
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
            await _context.Services.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Service entity, CancellationToken ct = default)
        {
            _context.Services.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Services.FirstOrDefaultAsync(s => s.Id == id, ct);
            if (entity is null) return;
            _context.Services.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<Service>> SearchAsync(string term, int skip, int take, CancellationToken ct = default)
        {
            term = (term ?? string.Empty).Trim();
            skip = Math.Max(0, skip);
            take = take <= 0 ? 50 : Math.Min(take, 200);

            return await _context.Services
                .AsNoTracking()
                .Where(s =>
                    EF.Functions.Like(s.Name, $"%{term}%") ||
                    EF.Functions.Like(s.Description ?? string.Empty, $"%{term}%"))
                .OrderBy(s => s.Name)
                .Skip(skip).Take(take)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Service>> SearchAsync(Guid categoryId, Guid cityId, int minRating, int skip, int take, CancellationToken ct = default)
        {
            minRating = Math.Clamp(minRating, 0, 5);
            skip = Math.Max(0, skip);
            take = take <= 0 ? 50 : Math.Min(take, 200);

            return await _context.Services
                .AsNoTracking()
                .Where(s => s.CategoryId == categoryId && s.CityId == cityId && s.AverageRating >= minRating)
                .OrderBy(s => s.Name)
                .Skip(skip).Take(take)
                .ToListAsync(ct);
        }
    }
}
