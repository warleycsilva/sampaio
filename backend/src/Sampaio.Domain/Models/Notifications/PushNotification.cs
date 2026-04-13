#nullable enable
using System;
using System.Collections.Generic;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.Models.Notifications
{
    /// <summary>
    /// Abstract class for PushNotification
    /// </summary>
    public abstract class PushNotification
    {
        protected PushNotification(
            string title,
            string body,
            string? topic = null,
            PayloadNotificationData? data = null)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(body))
            {
                throw new Exception("Non Nullable Property Exception");
            }

            Title = title;
            Body = body;
            Topic = topic;

            if (data != null)
            {
                Data = new Dictionary<string, string>
                {
                    {"event", data.Event.ToString()},
                    {"occurredAt", data.OccurredAt.ToString("o")},
                    {"title", data.Title ?? ""},
                    {"message", data.Message ?? ""},
                };

                if (data.Payload != null)
                {
                    Data.Add("payload", JsonUtils.Serialize(data.Payload));
                }
            }
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public string? Topic { get; set; }

        public Dictionary<string, string>? Data { get; set; }
    }
}
