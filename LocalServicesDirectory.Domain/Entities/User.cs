using System;
using LocalServicesDirectory.Domain.Core;

namespace LocalServicesDirectory.Domain.Entities
{
    
    public class User : Person
    {
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[]? PasswordSalt { get; set; }
        public bool IsProviderOwner { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}


