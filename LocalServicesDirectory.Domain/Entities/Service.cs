namespace LocalServicesDirectory.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid CityId { get; set; }
        public City? City { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? WebsiteUrl { get; set; }
        public bool IsVerified { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}


