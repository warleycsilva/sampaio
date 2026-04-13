using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.CommerceSegment;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.CommerceSegment;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class CommerceSegmentCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateCommerceSegmentCommand, CreateCommerceSegmentResult>,
        IRequestHandler<UpdateCommerceSegmentCommand, UpdateCommerceSegmentResult>,
        IRequestHandler<DeleteCommerceSegmentCommand, DeleteCommerceSegmentResult>
    {
        private readonly ICommerceSegmentRepository _commerceSegmentRepository;
        
        public CommerceSegmentCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            ICommerceSegmentRepository commerceSegmentRepository
            ) : base(uow, notifications)
        {
            _commerceSegmentRepository = commerceSegmentRepository;
        }

        public async Task<CreateCommerceSegmentResult> Handle(CreateCommerceSegmentCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateCommerceSegmentResult();

            var commerceSegment = CommerceSegment.New(command.Name, command.Description);

            await _commerceSegmentRepository.AddAsync(commerceSegment);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommerceSegmentCommandMessages.CreateFailed);
                return result;
            }

            result.CommerceSegmentId = commerceSegment.Id;
            result.Success = true;
            result.Message = CommerceSegmentCommandMessages.CreateSuccessful;

            return result;
        }

        public async Task<UpdateCommerceSegmentResult> Handle(UpdateCommerceSegmentCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateCommerceSegmentResult();

            var commerceSegment = await _commerceSegmentRepository.FindAsync(x => x.Id == command.Id);
            
            if (commerceSegment == null)
            {
                Notifications.Handle(CommerceSegmentCommandMessages.CommerceSegmentNotFound);
            }

            commerceSegment.Update(command.Name, command.Description);

            _commerceSegmentRepository.Modify(commerceSegment);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommerceSegmentCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = CommerceSegmentCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeleteCommerceSegmentResult> Handle(DeleteCommerceSegmentCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteCommerceSegmentResult();
            
            var commerceSegment = await _commerceSegmentRepository.FindAsync(x => x.Id == command.Id);
            
            if (commerceSegment == null)
            {
                Notifications.Handle(CommerceSegmentCommandMessages.CommerceSegmentNotFound);
            }

            commerceSegment.Delete();
            
            _commerceSegmentRepository.Modify(commerceSegment);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommerceSegmentCommandMessages.DeleteFailed);
                return result;
            }
            
            result.Success = true;
            result.Message = CommerceSegmentCommandMessages.DeleteSuccessful;
            
            return result;
        }
    }
}