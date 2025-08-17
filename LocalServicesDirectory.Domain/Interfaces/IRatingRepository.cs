using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Domain.Interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<double> GetAverageForServiceAsync(Guid serviceId);
        Task<IReadOnlyList<Rating>> GetForServiceAsync(Guid serviceId, int skip = 0, int take = 50);
    }
}

