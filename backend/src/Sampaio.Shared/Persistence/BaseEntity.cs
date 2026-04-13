using System;

namespace Sampaio.Shared.Persistence
{
    public class BaseEntity 
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public bool Deleted { get; protected set; }
    }
}
