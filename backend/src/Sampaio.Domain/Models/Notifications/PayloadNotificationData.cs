using System;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Models.Notifications
{
    public class PayloadNotificationData
    {
        public EPushNotificationEvent Event { get; set; }

        public DateTime OccurredAt { get; } = DateTime.UtcNow;

        public string Title { get; set; }

        public string Message { get; set; }

        public object Payload { get; set; }
    }
}