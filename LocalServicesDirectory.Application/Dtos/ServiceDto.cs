using System;

namespace LocalServicesDirectory.Application.Dtos
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; }
        public int AverageRating { get; set; }
    }
}
