using System;

namespace LocalServicesDirectory.Application.Dtos
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? WebsiteUrl { get; set; }
        public bool IsVerified { get; set; }

        public double AverageRating { get; set; }
    }
}
