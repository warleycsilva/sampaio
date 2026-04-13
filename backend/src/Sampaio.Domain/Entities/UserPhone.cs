using System;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Entities
{
    public class UserPhone : BaseEntity
    {

        public Guid UserId { get; private set; }

        [Newtonsoft.Json.JsonIgnore]
        public User User { get; private set; }

        public Phone Phone { get; private set; }
        
        public static UserPhone New(Guid userId, Phone phone) => new UserPhone
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Phone = phone,
            UserId = userId
        };
    }
}
