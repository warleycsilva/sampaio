#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace Sampaio.Domain.Models.Notifications
{
    public class MultiCastPushNotification : PushNotification
    {
        public MultiCastPushNotification(ICollection<DeviceInfo> devicesInfo, string title, string body, string? topic = null, PayloadNotificationData? data = null) : base(title, body, topic, data)
        {
            DevicesInfo = devicesInfo.ToList();
        }

        public ICollection<DeviceInfo> DevicesInfo { get; private set; } = new List<DeviceInfo>();
    }
}
