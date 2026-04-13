using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using Sampaio.Domain.Projections;
using Sampaio.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Sampaio.Domain.Commands.Commerce;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Commerce;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Security;
using Sampaio.Shared.Utils;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.CommandHandlers
{
    public class CommerceCommandHandler : BaseCommandHandler,
        IRequestHandler<CompleteCommerceSignUpCommand, CommerceSignupResult>,
        IRequestHandler<SelectCommerceSegmentCommand, SelectCommerceSegmentResult>,
        IRequestHandler<UpsertAddressCommand, BaseCommerceResult>,
        IRequestHandler<UpsertIdentificationCommand, BaseCommerceResult>,
        IRequestHandler<UpsertPhoneCommand, BaseCommerceResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly ICommerceRepository _commerceRepository;
        public CommerceCommandHandler(IUnitOfWork uow, IDomainNotification notifications
            , IUserRepository userRepository
            ,JwtTokenConfig jwtTokenConfig
            ,ICommerceRepository commerceRepository
            ) : base(uow, notifications)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _userRepository = userRepository;
            _commerceRepository = commerceRepository;
        }

        public async Task<CommerceSignupResult> Handle(CompleteCommerceSignUpCommand command, CancellationToken cancellationToken)
        {
            var result = new CommerceSignupResult();

            var include = new IncludeHelper<User>().Include(x => x.Commerce.Identification).Include(x => x.Phones).Includes;

            var user = await _userRepository.FindAsync(x => x.Id == command.SessionUser.Id, include);

            if (command.Phones != null)
            {
                user.ClearPhone();
                foreach (var phone in command.Phones)
                {
                    user.Phones.Add(UserPhone.New(user.Id, phone));
                }
            }

            if (await _userRepository.AnyAsync(x =>
                x.Commerce.Identification.Number == command.Identification.Number.Replace(".", "").Replace("-", "")))
            {
                Notifications.Handle("O CPF informado já está sendo utilizado.");
                return null;
            }
            
            user.UpdateCommerce(user.Commerce.Update(command.Identification, command.AddressInformation));

            _userRepository.Modify(user);

            if (!await CommitAsync())
            {
                return result;
            }

            var sessionUser = new SessionUser()
            {
                Email = user.Email,
                Id = user.Id,
                Type = EUserType.Commerce,
                AvatarUrl = user.AvatarUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                // AcceptedTerms = user.Commerce.TermsAreAccepted,
                Identification = user.Commerce?.Identification
            };

            result.User = sessionUser;

            result.AccessToken = _jwtTokenConfig.GenerateJwtToken(sessionUser.ClaimsPrincipal());
            result.Success = true;

            result.Message = CommerceCommandMessages.SignUpComplete;

            return result;
        }

        public async Task<SelectCommerceSegmentResult> Handle(SelectCommerceSegmentCommand command, CancellationToken cancellationToken)
        {
            var result = new SelectCommerceSegmentResult();

            var commerce = await _commerceRepository.FindAsync(x => x.Id == command.SessionUser.Id);

            if(commerce == null)
            {
                result.Message = CommerceCommandMessages.CommerceNotFound;
                return result;
            }
            commerce.SetSegment(command.CommerceSegmentId);
            
            _commerceRepository.Modify(commerce);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                return result;
            }
            
            result.Success = true;
            result.Message = CommerceCommandMessages.SegmentSelectionSuccessful;
            return result;
            
            
        }

        public async Task<BaseCommerceResult> Handle(UpsertAddressCommand command, CancellationToken cancellationToken)
        {
            var result = new BaseCommerceResult();
            var include = new IncludeHelper<User>().Include(x => x.Commerce.Identification).Include(x => x.Commerce.AddressInformation.City.State).Include(x => x.Phones).Includes;
            var user = await _userRepository.FindAsync(x => x.Id == command.UserId, include);

            if (string.IsNullOrEmpty(command?.Address?.Lat) || string.IsNullOrEmpty(command?.Address?.Long))
            {
                Notifications.Handle("Não foi possível obter sua localização!!!\n Verifique se as permissões de localização foram ativadas.");
                return result;
            }

            user.Commerce.UpdateAddress(command.Address);
            user.Commerce.UpdateLocation(command?.Address?.Lat, command?.Address?.Long);
            _userRepository.Modify(user);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                return result;
            }

            result.Commerce = user.Commerce.ToVm();
            result.Success = true;
            result.Message = CommerceCommandMessages.UpdatedSuccessfully;
            return result;
        }

        public async Task<BaseCommerceResult> Handle(UpsertIdentificationCommand command, CancellationToken cancellationToken)
        {
            var result = new BaseCommerceResult();
            var include = new IncludeHelper<User>().Include(x => x.Commerce.Identification).Include(x => x.Commerce.AddressInformation.City.State).Include(x => x.Phones).Includes;
            var user = await _userRepository.FindAsync(x => x.Id == command.UserId, include);

            user.Commerce.UpdateIdentification(command.Identification);
            _userRepository.Modify(user);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                return result;
            }

            result.Commerce = user.Commerce.ToVm();
            result.Success = true;
            result.Message = CommerceCommandMessages.UpdatedSuccessfully;
            return result;
        }

        public async Task<BaseCommerceResult> Handle(UpsertPhoneCommand command, CancellationToken cancellationToken)
        {
            var result = new BaseCommerceResult();
            var include = new IncludeHelper<User>().Include(x => x.Commerce.Identification).Include(x => x.Commerce.AddressInformation.City.State).Include(x => x.Phones).Includes;
            var user = await _userRepository.FindAsync(x => x.Id == command.UserId, include);

            if (string.IsNullOrEmpty(command.Phone))
            {
                Notifications.Handle("Telefone não pode ser vazio");
                return result;
            }   
            user.ClearPhone();
            user.Phones.Add(UserPhone.New(user.Id, new Phone()
            {
                Number = command.Phone,
                Type = EPhoneType.CellPhone
            }));
            _userRepository.Modify(user);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                return result;
            }

            result.Commerce = user.Commerce.ToVm();
            result.Success = true;
            result.Message = CommerceCommandMessages.UpdatedSuccessfully;
            return result;
        }
    }
}