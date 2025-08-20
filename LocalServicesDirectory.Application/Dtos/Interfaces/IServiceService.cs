using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocalServicesDirectory.Application.Dtos;

namespace LocalServicesDirectory.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> SearchAsync(string q, int skip, int take, CancellationToken ct = default);
        Task<IEnumerable<ServiceDto>> SearchAsync(Guid categoryId, Guid cityId, int minRating, int skip, int take, CancellationToken ct = default);

        Task<ServiceDto?> GetAsync(Guid id, CancellationToken ct = default);
        Task<Guid> CreateAsync(ServiceDto dto, CancellationToken ct = default);
        Task UpdateAsync(Guid id, ServiceDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}


