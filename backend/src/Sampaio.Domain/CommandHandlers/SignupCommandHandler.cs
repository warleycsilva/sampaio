using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.Signup;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.EmailsViewModels;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Signup;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class SignupCommandHandler : BaseCommandHandler,
        IRequestHandler<SignupCommand, SignupResult>
    {
        private readonly IPasswordHasherService _passwordHashService;
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly IJobsService _jobService;

        public SignupCommandHandler(IUnitOfWork uow,
            IDomainNotification notifications,
            IPasswordHasherService passwordHashService,
            IUserRepository userRepository, IJobsService jobService, JwtTokenConfig jwtTokenConfig)
            : base(uow, notifications)
        {
            _passwordHashService = passwordHashService;
            _userRepository = userRepository;
            _jobService = jobService;
            _jwtTokenConfig = jwtTokenConfig;
        }


        public async Task<SignupResult> Handle(SignupCommand command,
            CancellationToken cancellationToken)
        {
            if (command.FirstName.Contains("@"))
            {
                Notifications.Handle("Nome incorreto.");
                return null;
            }
            return command.Type switch
            {
                ESignupType.Backoffice => await HandleBackoffice(command),
                _ => new SignupResult {Message = "Tipo de cadastro ainda não disponível"}
            };
        }

        private async Task<SignupResult> HandleBackoffice(SignupCommand command)
        {
            var result = new SignupResult();
            
            var user = await _userRepository.FindAsync(x => x.Email.ToLower() == command.Email.ToLower());

            if (user != null)
            {
                if(!user.Active)
                {
                    result.Message = "Usuário existente. Verifique sua caixa de e-mail para ativar a conta.";
                    // _jobService.EnqueueSendNewUserEmail(new NewUserEmailVm(user.FullName,user.Email,user.ActivationToken));
                
                    return result;}
                else
                {
                    result.Message = "Usuário existente. Utilize a opção de recuperação de senha.";
                    return result;
                }
                result.Message = $"O e-mail {command.Email} não pode ser utilizado";
                return result;
            }

            user = User.NewBackoffice(
                BackofficeUser.New(),
                command.Email,
                _passwordHashService.Hash(command.Password),
                command.FirstName,
                command.LastName);

            var expirationDate = DateTime.UtcNow.AddDays(30);
            
            var token = _jwtTokenConfig.GenerateJwtToken(
                new[] {new Claim(CustomClaims.Id, user.Id.ToString())}, expirationDate
            );
            
            user.SetValidationToke(token);
            
            await _userRepository.AddAsync(user);

            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            // _jobService.EnqueueSendNewUserEmail(new NewUserEmailVm(user.FullName,user.Email,user.ActivationToken));
            
            result.Success = true;

            result.Message = SignupCommandMessages.BackofficeSignUpSucess;

            return result;
        }
    }
}