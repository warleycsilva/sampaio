using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.DebtPayment;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.DebtPayment;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class DebtPaymentCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateDebtPaymentCommand, CreateDebtPaymentResult>,
        IRequestHandler<UpdateDebtPaymentCommand, UpdateDebtPaymentResult>,
        IRequestHandler<DeleteDebtPaymentCommand, DeleteDebtPaymentResult>
    {
        private readonly IDebtPaymentRepository _debtPaymentRepository;
        
        public DebtPaymentCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IDebtPaymentRepository debtPaymentRepository
            ) : base(uow, notifications)
        {
            _debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<CreateDebtPaymentResult> Handle(CreateDebtPaymentCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateDebtPaymentResult();

            var debtPayment = DebtPayment.New(command.DebtId, command.Status, command.ExternalCode);

            await _debtPaymentRepository.AddAsync(debtPayment);

            if (!await CommitAsync())
            {
                Notifications.Handle(DebtPaymentCommandMessages.CreateFailed);
                return result;
            }
            
            result.DebtPaymentId = debtPayment.Id;
            result.Success = true;
            result.Message = DebtPaymentCommandMessages.CreateSuccessful;
            
            return result;
        }

        public async Task<UpdateDebtPaymentResult> Handle(UpdateDebtPaymentCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateDebtPaymentResult();

            var debtPayment = await _debtPaymentRepository.FindAsync(x => x.Id == command.Id);
            
            if (debtPayment == null)
            {
                Notifications.Handle(DebtPaymentCommandMessages.DebtPaymentNotFound);
            }

            debtPayment.Update(command.DebtId, command.Status, command.ExternalCode);
            
            _debtPaymentRepository.Modify(debtPayment);

            if (!await CommitAsync())
            {
                Notifications.Handle(DebtPaymentCommandMessages.UpdateFailed);
                return result;
            }
            
            result.Success = true;
            result.Message = DebtPaymentCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeleteDebtPaymentResult> Handle(DeleteDebtPaymentCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteDebtPaymentResult();

            var debtPayment = await _debtPaymentRepository.FindAsync(x => x.Id == command.Id);
            
            if (debtPayment == null)
            {
                Notifications.Handle(DebtPaymentCommandMessages.DebtPaymentNotFound);
            }
            
            debtPayment.Delete();
            
            _debtPaymentRepository.Modify(debtPayment);

            if (!await CommitAsync())
            {
                Notifications.Handle(DebtPaymentCommandMessages.DeleteFailed);
                return result;
            }
            
            result.Success = true;
            result.Message = DebtPaymentCommandMessages.DeleteSuccessful;

            return result;
        }
    }
}