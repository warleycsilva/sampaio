using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Shared.Config;
using MediatR;
using Sampaio.Domain.Commands.Notification;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Models;
using Sampaio.Domain.Models.Notifications;
using Sampaio.Domain.Results.Notification;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.CommandHandlers
{
    public class NotificationCommandHandler: BaseCommandHandler,
        IRequestHandler<SendNotificationCommand, SendNotificationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IUserDeviceRepository _deviceRepository;
        private readonly IPushNotificationService _notificationService;
        
        public NotificationCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IUserRepository userRepository
            ,IPushNotificationService notificationService
            , IUserDeviceRepository deviceRepository, IDriverRepository driverRepository) : base(uow, notifications)
        {
            _deviceRepository = deviceRepository;
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
        }

        public async Task<SendNotificationResult> Handle(SendNotificationCommand command, CancellationToken cancellationToken)
        {
            var result = new SendNotificationResult();

            var includes = new IncludeHelper<UserDevice>()
                .Include(x => x.User.Driver)
                .Include(x => x.User.Commerce)
                .Includes;
            
            var devices = _deviceRepository.ListAsNoTracking(d => !d.Deleted, includes: includes);

            try
            {
                var devicesToSendNotification = _deviceRepository.ListAsNoTracking(x => x.Deleted == false);
                
                if (!command.SendToCommerces && command.SendToDrivers)
                {
                    devicesToSendNotification = _deviceRepository.ListAsNoTracking(x => x.User.Driver != null && !x.Deleted);
                }
                else if (command.SendToCommerces && !command.SendToDrivers)
                {
                    devicesToSendNotification = _deviceRepository.ListAsNoTracking(x => x.User.Commerce != null && !x.Deleted);
                }
                    
                var count = devicesToSendNotification.Count();
                var devicesInfo = new List<DeviceInfo>();

                foreach (var device in devicesToSendNotification)
                {
                    devicesInfo.Add(new DeviceInfo()
                    {
                        DeviceId = device.DeviceId,
                        DeviceToken = device.DeviceToken
                    });
                }
                
                _notificationService.SendMessage(new MultiCastPushNotification(
                    devicesInfo,
                    command.Title,
                    command.Message,
                    data: new PayloadNotificationData()
                    {
                        Title = command.Title,
                        Message = command.Message,
                        Event = EPushNotificationEvent.Backoffice,
                    }
                ));
            }
            catch (Exception e)
            {
                Notifications.Handle(e.Message);
                return result;
            }

            result.Success = true;
            result.Message = "Notificação enviada com sucesso.";
            return result;
        }
    }
}