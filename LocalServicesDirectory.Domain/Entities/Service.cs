using System;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class Service : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public Guid CityId { get; set; }
        public City City { get; set; } = null!;

        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? WebsiteUrl { get; set; }

        
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int AverageRating { get; set; }
    }
}


