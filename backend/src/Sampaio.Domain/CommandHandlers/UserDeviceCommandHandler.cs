using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Models.Notifications;
using Sampaio.Domain.Commands.UserDevice;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.UserDevice;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class UserDeviceCommandHandler :BaseCommandHandler,
        IRequestHandler<AddUserDeviceCommand,UserDeviceBaseResult>
    {
        
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IPushNotificationService _pushNotificationService;
        public UserDeviceCommandHandler(
            IUserDeviceRepository userDeviceRepository,
            IPushNotificationService pushNotificationService,
            IUnitOfWork uow, IDomainNotification notifications) : base(uow, notifications)
        {
            _userDeviceRepository = userDeviceRepository;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<UserDeviceBaseResult> Handle(AddUserDeviceCommand command, CancellationToken cancellationToken)
        {
            var result = new UserDeviceBaseResult();

            if (_userDeviceRepository.Any(x => x.DeviceId == command.DeviceId && x.DeviceToken == command.DeviceToken))
            {
                result.Success = true;
                result.Message = UserDeviceCommandMessages.AlreadyExists;
                return result;
            }
            
            var device = UserDevice.New(command.SessionUser.Id, command.DeviceId, command.DeviceToken);
            await _userDeviceRepository.AddAsync(device);
            
            if (!await CommitAsync())
            {
                Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                return result;
            }
            result.UserDeviceId = device.Id;
            result.Success = true;
            result.Message = UserDeviceCommandMessages.CreatedSuccessfully;
            return result;
        }
    }
}