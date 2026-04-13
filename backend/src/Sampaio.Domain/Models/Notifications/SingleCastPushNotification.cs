#nullable enable
namespace Sampaio.Domain.Models.Notifications
{
    public class SingleCastPushNotification : PushNotification
    {
        public SingleCastPushNotification(
            string deviceId,
            string deviceToken,
            string title,
            string body,
            string? topic = null,
            PayloadNotificationData? data = null) : base(title, body, topic, data)
        {
            DeviceId = deviceId;
            DeviceToken = deviceToken;
        }

        public string DeviceId { get; private set; }

        public string DeviceToken { get; private set; }
    }
}
