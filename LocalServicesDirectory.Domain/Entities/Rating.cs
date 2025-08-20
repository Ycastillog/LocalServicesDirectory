using System;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class Rating : IEntity
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public int Score { get; set; }          
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Service Service { get; set; } = null!;
    }
}


