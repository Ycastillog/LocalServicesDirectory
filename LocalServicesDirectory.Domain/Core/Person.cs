using System;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Core
{
    
    public abstract class Person : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public string FullName() => $"{FirstName} {LastName}".Trim();
    }
}


