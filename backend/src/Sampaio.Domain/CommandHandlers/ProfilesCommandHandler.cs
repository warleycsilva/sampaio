// ,using System;
// using System.IO;
// using System.Linq;
// using System.Security.Claims;
// using System.Threading;
// using System.Threading.Tasks;
// using MediatR;
// using Microsoft.AspNetCore.StaticFiles;
// using Sampaio.Shared.Extensions;
// using Sampaio.Domain.Commands.Profiles;
// using Sampaio.Domain.CommandsMessages;
// using Sampaio.Domain.Contracts.Infra;
// using Sampaio.Domain.Contracts.Repositories;
// using Sampaio.Domain.Contracts.Services;
// using Sampaio.Domain.EmailsViewModels;
// using Sampaio.Domain.Entities;
// using Sampaio.Domain.Results.Profiles;
// using Sampaio.Shared.Config;
// using Sampaio.Shared.Constants;
// using Sampaio.Shared.Enums;
// using Sampaio.Shared.Notifications;
// using Sampaio.Shared.Persistence;
// using Sampaio.Shared.Security;
// using Sampaio.Shared.Utils;
//
// namespace Sampaio.Domain.CommandHandlers
// {
//     public class ProfilesCommandHandler : BaseCommandHandler,
//         IRequestHandler<ResetPasswordRequestCommand, ResetPasswordRequestResult>,
//         IRequestHandler<ResetPasswordCommand, ResetPasswordResult>,
//         IRequestHandler<ActiveAccountCommand, ActiveAccountResult>,
//         IRequestHandler<ChangePasswordCommand, ResetPasswordResult>,
//         IRequestHandler<UpdateUserProfileCommand, UpdateUserProfileResult>,
//         IRequestHandler<ChangeProfilePhotoCommand, ChangeProfilePhotoResult>,
//         IRequestHandler<ChangeAddressCommand, ChangeAddressResult>,
//         IRequestHandler<AcceptTermsCommand, AcceptTermsResult>
//     {
//         private readonly IUserRepository _userRepository;
//         private readonly IJobsService _jobsService;
//         private readonly IPasswordHasherService _passwordHasherService;
//         private readonly JwtTokenConfig _jwtTokenConfig;
//         private readonly IStorageService _storageService;
//
//         public ProfilesCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
//             IUserRepository userRepository, IJobsService jobsService, IPasswordHasherService passwordHasherService,
//             JwtTokenConfig jwtTokenConfig, IStorageService storageService) : base(uow, notifications)
//         {
//             _userRepository = userRepository;
//             _jobsService = jobsService;
//             _passwordHasherService = passwordHasherService;
//             _jwtTokenConfig = jwtTokenConfig;
//             _storageService = storageService;
//         }
//
//         public async Task<ResetPasswordRequestResult> Handle(ResetPasswordRequestCommand command,
//             CancellationToken cancellationToken)
//         {
//             var result = new ResetPasswordRequestResult();
//
//             var user = await _userRepository.FindAsync(x => x.Email.ToLower() == command.Email.ToLower());
//
//             if (user == null)
//             {
//                 Notifications.Handle(ProfileCommandMessages.UserNotFound);
//                 return result;
//             }
//
//             if (!string.IsNullOrWhiteSpace(user.PasswordRecoverToken) &&
//                 _jwtTokenConfig.ValidateToken(user.PasswordRecoverToken) != null)
//             {
//                 // _jobsService.EnqueueSendResetPasswordTokenEmail(
//                 //     new ResetPasswordEmailVm(
//                 //         user.FullName,
//                 //         user.Email,
//                 //         user.PasswordRecoverToken));
//
//                 result.Message = ProfileCommandMessages.ReSendEmailSuccess;
//                 result.Success = true;
//                 return result;
//             }
//
//             var token = _jwtTokenConfig.GenerateJwtToken(
//                 new[] { new Claim(CustomClaims.Id, user.Id.ToString()) },
//                 DateTime.UtcNow.AddDays(2));
//
//             user.NewPasswordRecoverToken(token);
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//                 return result;
//             }
//
//             // _jobsService.EnqueueSendResetPasswordTokenEmail(
//             //     new ResetPasswordEmailVm(
//             //         user.FullName,
//             //         user.Email,
//             //         user.PasswordRecoverToken));
//             result.Success = true;
//             result.Message = ProfileCommandMessages.SendEmailSuccess;
//
//             return result;
//         }
//
//         public async Task<ResetPasswordResult> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
//         {
//             var result = new ResetPasswordResult();
//
//             var principal = _jwtTokenConfig.ValidateToken(command.Token);
//
//             if (principal == null)
//             {
//                 Notifications.Handle(ProfileCommandMessages.InvalidToken);
//                 return null;
//             }
//
//             // if (!Guid.TryParse(principal.Claims.FirstOrDefault(x => x.Type == CustomClaims.Id)?.Value ?? "",
//             //     out var id))
//             // {
//             //     Notifications.Handle(ProfileCommandMessages.InvalidToken);
//             //     return null;
//             // }
//
//             var user = await _userRepository.FindAsync(x => x.Id == Guid.NewGuid());
//
//             if (user == null)
//             {
//                 Notifications.Handle(ProfileCommandMessages.UserNotFound);
//                 return null;
//             }
//
//             if (!user.Active)
//             {
//                 Notifications.Handle(ProfileCommandMessages.UserInactive);
//                 return null;
//             }
//
//             if (user.PasswordRecoverToken != command.Token)
//             {
//                 Notifications.Handle(ProfileCommandMessages.InvalidToken);
//                 return null;
//             }
//
//             user.UpdatePasswordWithRecoverToken(_passwordHasherService.Hash(command.NewPassword));
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//                 return result;
//             }
//
//             result.Success = true;
//
//             result.Message = ProfileCommandMessages.PasswordChangeSuccess;
//             return result;
//         }
//
//         public async Task<ActiveAccountResult> Handle(ActiveAccountCommand command, CancellationToken cancellationToken)
//         {
//             var result = new ActiveAccountResult();
//
//             var principal = _jwtTokenConfig.ValidateToken(command.Token);
//
//             if (principal == null)
//             {
//                 Notifications.Handle(ProfileCommandMessages.InvalidToken);
//                 return result;
//             }
//
//             // if (!Guid.TryParse(principal.Claims.FirstOrDefault(x => x.Type == CustomClaims.Id)?.Value ?? "",
//             //     out var id))
//             // {
//             //     Notifications.Handle(ProfileCommandMessages.InvalidToken);
//             //     return result;
//             // }
//
//             var user = await _userRepository.FindAsync(x => x.Id == Guid.Empty);
//
//             if (user == null)
//             {
//                 Notifications.Handle(ProfileCommandMessages.UserNotFound);
//                 return result;
//             }
//
//             if (user.Active)
//             {
//                 Notifications.Handle(ProfileCommandMessages.AlreadyActivedUser);
//                 return result;
//             }
//
//             if (user.ActivationToken != command.Token)
//             {
//                 Notifications.Handle(ProfileCommandMessages.InvalidToken);
//                 return result;
//             }
//
//             user.ActivateAccount();
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//                 return result;
//             }
//
//             result.Success = true;
//
//             result.Message = ProfileCommandMessages.ActiveAccountSuccess;
//             return result;
//         }
//
//         public async Task<ResetPasswordResult> Handle(ChangePasswordCommand command,
//             CancellationToken cancellationToken)
//         {
//             var result = new ResetPasswordResult();
//
//             var user = await _userRepository.FindAsync(x => x.Id == command.SessionUser.Id);
//
//             if (!_passwordHasherService.Check(user.Password, command.CurrentPassword))
//             {
//                 Notifications.Handle(CommonMessages.InvalidCredentials);
//                 return result;
//             }
//
//             user.UpdatePassword(_passwordHasherService.Hash(command.NewPassword));
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//                 return result;
//             }
//
//             result.Success = true;
//
//             result.Message = ProfileCommandMessages.PasswordChangeSuccess;
//             return result;
//         }
//
//         public async Task<AcceptTermsResult> Handle(AcceptTermsCommand command, CancellationToken cancellationToken)
//         {
//             var result = new AcceptTermsResult();
//
//             var includes = new IncludeHelper<User>()
//                 .Include(x => x.Commerce)
//                 .Include(x => x.Driver)
//                 .Includes;
//
//             var user = await _userRepository.FindAsync(x => x.Id == command.SessionUser.Id, includes);
//
//             user.AcceptTerms();
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//
//                 return result;
//             }
//
//             var sessionUser = new SessionUser()
//             {
//                 Email = user.Email,
//                 Id = user.Id,
//                 Type = user.Driver != null ? EUserType.Driver : EUserType.Commerce,
//                 AvatarUrl = user.AvatarUrl,
//                 FirstName = user.FirstName,
//                 LastName = user.LastName,
//                 // AcceptedTerms = user.Establishment != null ? user?.Establishment?.TermsAreAccepted : user?.Lessee?.TermsAreAccepted,
//                 // Identification = user.Establishment != null ? user?.Establishment?.Identification : user?.Lessee?.Identification
//             };
//
//             result.User = sessionUser;
//             result.AccessToken = _jwtTokenConfig.GenerateJwtToken(sessionUser.ClaimsPrincipal());
//             result.Success = true;
//             result.Message = "Termos aceitos com sucesso.";
//             return result;
//         }
//
//         public async Task<UpdateUserProfileResult> Handle(UpdateUserProfileCommand command,
//             CancellationToken cancellationToken)
//         {
//             var result = new UpdateUserProfileResult();
//
//             var user = await _userRepository.FindAsync(x => x.Id == command.SessionUser.Id);
//
//             if (user == null)
//             {
//                 result.Message = ProfileCommandMessages.UserNotFound;
//                 return result;
//             }
//
//             user.Update(command.FirstName, command.Email, command.LastName);
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//                 return result;
//             }
//
//             result.Message = ProfileCommandMessages.UpdateSuccess;
//             result.Success = true;
//             return result;
//         }
//
//         public async Task<ChangeProfilePhotoResult> Handle(ChangeProfilePhotoCommand command,
//             CancellationToken cancellationToken)
//         {
//             var result = new ChangeProfilePhotoResult();
//             var avatar = string.Empty;
//             var includes = new IncludeHelper<User>()
//                 .Include(x => x.Driver)
//                 .Include(x => x.Commerce)
//                 .Includes;
//
//             var user = await _userRepository.FindAsync(x => x.Id == command.SessionUser.Id, includes);
//
//             if (user == null)
//             {
//                 result.Message = ProfileCommandMessages.UserNotFound;
//                 return result;
//             }
//
//             //Azure
//             if (command.FileInput?.HasValue() == true)
//             {
//                 var fileName = $"{user.Id}-{Guid.NewGuid().HasDigits()}{Path.GetExtension(command.FileInput.Name)}";
//                 string contentType;
//                 new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
//                 avatar = await _storageService.UploadAsync(command.FileInput.Buffer, fileName, contentType, true);
//
//                 if (avatar.IsNull())
//                 {
//                     Notifications.Handle("Erro ao salvar Imagem");
//                     return null;
//                 }
//
//                 user.UpdateAvatarUrl(avatar);
//             }
//
//             _userRepository.Modify(user);
//
//             if (!await CommitAsync())
//             {
//                 Notifications.Handle(ProfileCommandMessages.ProcessingError);
//                 return result;
//             }
//
//             result.Message = ProfileCommandMessages.UpdatePhotoSuccess;
//             result.Success = true;
//             return result;
//         }
//
//         public async Task<ChangeAddressResult> Handle(ChangeAddressCommand command, CancellationToken cancellationToken)
//         {
//             var result = new ChangeAddressResult();
//             return result;
//         }
//     }
// }