using System;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.ValueObjects;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class BackofficeUser : BaseEntity
    {
        public User User { get; private set; }
        public static BackofficeUser New() => new BackofficeUser()
        {
            CreatedAt = DateTime.UtcNow,
        };
        
    }
}
