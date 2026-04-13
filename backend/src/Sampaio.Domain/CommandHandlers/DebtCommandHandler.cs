using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Commands.Debt;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Debt;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class DebtCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateDebtCommand, CreateDebtResult>,
        IRequestHandler<UpdateDebtCommand, UpdateDebtResult>,
        IRequestHandler<DeleteDebtCommand, DeleteDebtResult>
    {
        private readonly IDebtRepository _debtRepository;

        public DebtCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IDebtRepository debtRepository) : base(uow, notifications)
        {
            _debtRepository = debtRepository;
        }

        public async Task<CreateDebtResult> Handle(CreateDebtCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateDebtResult();

            var debt = Debt.New(command.Value, command.DriverId, command.Status, command.Type, null, command.PlanId);

            await _debtRepository.AddAsync(debt);

            if (!await CommitAsync())
            {
                Notifications.Handle(DebtCommandMessages.CreateFailed);
                return result;
            }

            result.DebtId = debt.Id;
            result.Success = true;
            result.Message = DebtCommandMessages.CreateSuccessful;

            return result;
        }

        public async Task<UpdateDebtResult> Handle(UpdateDebtCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateDebtResult();

            var debt = await _debtRepository.FindAsync(x => x.Id == command.Id);
            
            if (debt == null)
            {
                Notifications.Handle(DebtCommandMessages.DebtNotFound);
            }

            debt.Update(command.Value, command.DriverId, command.Status, command.Type);

            _debtRepository.Modify(debt);

            if (!await CommitAsync())
            {
                Notifications.Handle(DebtCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = DebtCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeleteDebtResult> Handle(DeleteDebtCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteDebtResult();

            var debt = await _debtRepository.FindAsync(x => x.Id == command.Id);
            
            if (debt == null)
            {
                Notifications.Handle(DebtCommandMessages.DebtNotFound);
            }

            debt.Delete();

            _debtRepository.Modify(debt);

            if (!await CommitAsync())
            {
                Notifications.Handle(DebtCommandMessages.DeleteFailed);
                return result;
            }

            result.Success = true;
            result.Message = DebtCommandMessages.DeleteSuccessful;

            return result;
        }
    }
}
