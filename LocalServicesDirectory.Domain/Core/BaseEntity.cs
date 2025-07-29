using System;

namespace LocalServicesDirectory.Domain.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}


