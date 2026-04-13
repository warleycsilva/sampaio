using System;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.Models.Notifications;
using Sampaio.Shared.Config;

namespace Sampaio.Infra
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly ILogger _logger;

        public PushNotificationService(AppConfig appConfig, ILogger logger)
        {
            _logger = logger;
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(appConfig.FirebaseCredentials)
            });
        }

        public Task SendMessage(SingleCastPushNotification pushNotification)
        {
            try
            {
                FirebaseMessaging.DefaultInstance.SendAsync(new Message
                {
                    Token = pushNotification.DeviceToken,
                    Notification = new Notification
                    {
                        Title = pushNotification.Title,
                        Body = pushNotification.Body
                    },
                    Data = pushNotification.Data,
                    Topic = pushNotification.Topic,
                    Android = new AndroidConfig
                    {
                        Priority = Priority.High
                    },
                });
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.Error($"Erro ao enviar notificação para device id {pushNotification.DeviceId}",e);
                return Task.CompletedTask;
            }
        }

        public Task SendMessage(MultiCastPushNotification pushNotification)
        {
            try
            {
                FirebaseMessaging.DefaultInstance.SendMulticastAsync(new MulticastMessage
                {
                    Tokens = pushNotification.DevicesInfo.Select(x => x.DeviceToken).ToArray(),
                    Notification = new Notification
                    {
                        Title = pushNotification.Title,
                        Body = pushNotification.Body,
                    },
                    Data = pushNotification.Data,
                    Android = new AndroidConfig
                    {
                        Priority = Priority.High
                    },
                });
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.Error($"Erro ao enviar notificação para devices id's ({string.Join(",", pushNotification.DevicesInfo.Select(x => x.DeviceId))})",e);
                return Task.CompletedTask;
            }
        }

    }
}
