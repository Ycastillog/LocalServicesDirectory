using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? State { get; set; }
        public string Country { get; set; } = "República Dominicana";
    }
}

