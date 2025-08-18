namespace LocalServicesDirectory.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }

        public Guid UserId { get; set; }
        public byte Score { get; set; }
        public string? Comment { get; set; }
    }
}

