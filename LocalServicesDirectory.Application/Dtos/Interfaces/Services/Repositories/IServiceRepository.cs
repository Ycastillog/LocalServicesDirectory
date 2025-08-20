using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Application.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync(CancellationToken ct = default);
        Task<Service?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Service entity, CancellationToken ct = default);
        Task UpdateAsync(Service entity, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);

        Task<IEnumerable<Service>> SearchAsync(string term, int skip, int take, CancellationToken ct = default);
        Task<IEnumerable<Service>> SearchAsync(Guid categoryId, Guid cityId, int minRating, int skip, int take, CancellationToken ct = default);
    }
}




