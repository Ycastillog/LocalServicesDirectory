using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Domain.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IReadOnlyList<Service>> SearchAsync(
            string? text,
            Guid? categoryId,
            Guid? cityId,
            int minRating = 0,
            int skip = 0,
            int take = 50);
    }
}
