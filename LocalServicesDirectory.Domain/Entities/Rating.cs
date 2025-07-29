using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
    }
}

