using System;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    
    public class Rating : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }

        private byte _score = 1;
        public byte Score
        {
            get => _score;
            set
            {
                if (value < 1 || value > 5) throw new ArgumentOutOfRangeException(nameof(Score), "Score must be 1..5");
                _score = value;
            }
        }

        public string? Comment { get; set; }
    }
}


