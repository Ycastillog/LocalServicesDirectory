namespace LocalServicesDirectory.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}



