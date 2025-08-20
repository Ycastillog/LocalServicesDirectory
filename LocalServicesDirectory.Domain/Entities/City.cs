using System;
using System.Collections.Generic;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class City : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}


