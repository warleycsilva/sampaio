using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.BackofficeUser;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.BackofficeUser;
using Sampaio.Shared.Config;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Security;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.CommandHandlers
{
    public class BackofficeUserCommandHandler : BaseCommandHandler,
        IRequestHandler<CompleteBackofficeSignUpCommand, CompleteBackofficeSignUpResult>
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly IUserRepository _userRepository;
        
        protected BackofficeUserCommandHandler(IUnitOfWork uow,JwtTokenConfig jwtTokenConfig ,IDomainNotification notifications
            , IUserRepository userRepository) : base(uow, notifications)
        { _jwtTokenConfig = jwtTokenConfig;
            _userRepository = userRepository;
        }

        public async Task<CompleteBackofficeSignUpResult> Handle(CompleteBackofficeSignUpCommand command, CancellationToken cancellationToken)
        {
             var result = new CompleteBackofficeSignUpResult();

            var include = new IncludeHelper<User>().Include(x => x.BackofficeUser).Include(x => x.Phones).Includes;

            var user = await _userRepository.FindAsync(x => x.Id == command.SessionUser.Id, include);

            if (command.Phones != null)
            {
                foreach (var phone in command.Phones)
                {
                    user.Phones.Add(UserPhone.New(user.Id, phone));
                }
            }

            user.UpdateBackofficeUser(user.BackofficeUser);

            _userRepository.Modify(user);

            if (!await CommitAsync())
            {
                return result;
            }

            var sessionUser = new SessionUser()
            {
                Email = user.Email,
                Id = user.Id,
                Type = EUserType.Backoffice,
                AvatarUrl = user.AvatarUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            result.User = sessionUser;

            result.AccessToken = _jwtTokenConfig.GenerateJwtToken(sessionUser.ClaimsPrincipal());
            result.Success = true;

            result.Message = BackofficeCommandMessages.CompleteSignUpSuccess;

            return result;
        }
    }
}