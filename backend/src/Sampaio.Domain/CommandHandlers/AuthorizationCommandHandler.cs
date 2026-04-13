using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.Auth;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.EmailsViewModels;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Auth;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Security;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.CommandHandlers
{
    public class AuthorizationCommandHandler : BaseCommandHandler,
        IRequestHandler<AuthorizeUserCommand, AuthorizeUserResult>,
        IRequestHandler<AuthorizeBackofficeCommand, AuthorizeBackofficeResult>
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJobsService _jobService;

        public AuthorizationCommandHandler(IUnitOfWork uow,
            IDomainNotification notifications,
            JwtTokenConfig jwtTokenConfig,
            IUserRepository userRepository,
            IJobsService jobService,
            IPasswordHasherService passwordHasherService)
            : base(uow, notifications)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _jobService = jobService;
        }

        public async Task<AuthorizeUserResult> Handle(AuthorizeUserCommand command,
            CancellationToken cancellationToken)
        {
            var result = new AuthorizeUserResult();
            var includes = new IncludeHelper<User>()
                .Include(x => x.BackofficeUser).Includes;
            var user = await _userRepository.FindAsync(x => x.Email.ToLower() == command.Email.ToLower() && !x.Deleted, includes);

            if (user == null)
            {
                result.Error = CommonMessages.InvalidCredentials;
                return result;
            }

            if (!user.Active)
            {
                var token = _jwtTokenConfig.GenerateJwtToken(
                    new[] {new Claim(CustomClaims.Id, user.Id.ToString())},
                    DateTime.UtcNow.AddHours(48));
                user.SetValidationToke(token);
                _userRepository.Modify(user);

                if (!await CommitAsync())
                {
                    Notifications.Handle("Conta inativada. Falha ao gerar novo token.");
                    return result;
                }
                
                // _jobService.EnqueueSendNewUserEmail(new NewUserEmailVm(user.FullName,user.Email,user.ActivationToken));
                result.Error = ProfileCommandMessages.UserInactiveFriendly;
                return result;
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.Error = CommonMessages.InvalidCredentials;
                return result;
            }

            if (!_passwordHasherService.Check(user.Password, command.Password))
            {
                result.Error = CommonMessages.InvalidCredentials;
                return result;
            }
             if (user.BackofficeUser != null)
            {
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
            }

            result.Success = true;
            return result;
        }

        private async Task<AuthorizeUserResult> HandleDriver(User user)
        {
            var result = new AuthorizeUserResult();

            var sessionUser= new SessionUser()
            {
            Email = user.Email,
            Id = user.Id,
            Type = EUserType.Driver,
            AvatarUrl = user.AvatarUrl,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Identification = user.Driver?.Identification,
            };

            result.User = sessionUser;

            result.AccessToken = _jwtTokenConfig.GenerateJwtToken(sessionUser.ClaimsPrincipal());

            return result;

        }
        private async Task<AuthorizeUserResult> HandleCommerce(User user)
        {
            var result = new AuthorizeUserResult();

            var sessionUser = new SessionUser()
            {
                Email = user.Email,
                Id = user.Id,
                Type = EUserType.Commerce,
                AvatarUrl = user.AvatarUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Identification = user.Commerce.Identification,
            };

            result.User = sessionUser;

            result.AccessToken = _jwtTokenConfig.GenerateJwtToken(sessionUser.ClaimsPrincipal());
            return result;
        }
        private async Task<AuthorizeUserResult> HandleBackoffice(User user)
        {
            var result = new AuthorizeUserResult();

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
            return result;
        }

        public async Task<AuthorizeBackofficeResult> Handle(AuthorizeBackofficeCommand command, CancellationToken cancellationToken)
        {
            var result = new AuthorizeBackofficeResult();

            var includes = new IncludeHelper<User>().Include(x => x.BackofficeUser).Includes;
            var user = await _userRepository.FindAsync(x => string.Equals(x.Email.ToLower(), command.Username.ToLower()) && !x.Deleted, includes);

            if (user == null)
            {
                result.Error = CommonMessages.InvalidCredentials;
                return result;
            }

            if (!user.Active)
            {
                result.Error = ProfileCommandMessages.UserInactiveFriendly;
                return result;
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.Error = CommonMessages.InvalidCredentials;
                return result;
            }

            if (!_passwordHasherService.Check(user.Password, command.Password))
            {
                result.Error = CommonMessages.InvalidCredentials;
                return result;
            }

            if (user.BackofficeUser != null)
            {
                var sessionUser = new SessionUser()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Type = EUserType.Backoffice,
                    AvatarUrl = user.AvatarUrl,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                result.User = sessionUser;

                result.AccessToken = _jwtTokenConfig.GenerateJwtToken(sessionUser.ClaimsPrincipal());
            }
            else
            {
                result.Error = CommonMessages.InvalidProfile;
                return result;
            }

            result.Success = true;
            return result;
        }
    }
}
