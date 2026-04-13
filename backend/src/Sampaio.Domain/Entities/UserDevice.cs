using System;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class UserDevice : BaseEntity
    {
        protected UserDevice()
        {
        }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public string DeviceId { get; private set; }
        public string DeviceToken { get; private set; }
        
        public static UserDevice New(Guid userId, string deviceId, string deviceToken) => new UserDevice
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UserId = userId,
            DeviceId = deviceId,
            DeviceToken = deviceToken
        };

        public void Update(string deviceId, string deviceToken)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
        }
    }
}