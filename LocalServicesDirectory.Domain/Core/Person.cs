using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProviderId { get; set; }
    }
}

