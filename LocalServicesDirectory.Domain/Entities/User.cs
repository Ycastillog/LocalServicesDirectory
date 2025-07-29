using System;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    public class User : Person
    {
        public string Email { get; set; }
        public bool IsProvider { get; set; }
    }
}

