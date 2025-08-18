using LocalServicesDirectory.Domain.Core;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Repositories
{
    public class BaseRepository<T> where T : class, IEntity
    {
        protected readonly LocalServicesContext _ctx;
        protected readonly DbSet<T> _set;

        public BaseRepository(LocalServicesContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _set.FirstOrDefaultAsync(e => e.Id == id, ct);

        public virtual async Task<List<T>> GetAllAsync(CancellationToken ct = default)
            => await _set.ToListAsync(ct);

        public virtual async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            _set.Add(entity);
            await _ctx.SaveChangesAsync(ct);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _set.Update(entity);
            await _ctx.SaveChangesAsync(ct);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity is null) return;
            _set.Remove(entity);
            await _ctx.SaveChangesAsync(ct);
        }
    }
}

