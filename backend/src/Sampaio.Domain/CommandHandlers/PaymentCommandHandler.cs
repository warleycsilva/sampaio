using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.Payment;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Payment;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class PaymentCommandHandler : BaseCommandHandler,
        IRequestHandler<CreatePaymentCommand, CreatePaymentResult>,
        IRequestHandler<UpdatePaymentCommand, UpdatePaymentResult>,
        IRequestHandler<DeletePaymentCommand, DeletePaymentResult>
    {
        private readonly IPaymentRepository _paymentRepository;
        
        public PaymentCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IPaymentRepository paymentRepository) : base(uow, notifications)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var result = new CreatePaymentResult();

            var payment = Payment.New(command.DebtId, command.Status);
            
            await _paymentRepository.AddAsync(payment);

            if (!await CommitAsync())
            {
                Notifications.Handle(PaymentCommandMessages.CreateFailed);
                return result;
            }
            
            result.PaymentId = payment.Id;
            result.Success = true;
            result.Message = PaymentCommandMessages.CreateSuccessful;
            
            return result;
        }

        public async Task<UpdatePaymentResult> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdatePaymentResult();
            
            var payment = await _paymentRepository.FindAsync(x => x.Id == command.Id);

            if (payment == null)
            {
                Notifications.Handle(PaymentCommandMessages.PaymentNotFound);
            }

            payment.Update(command.DebtId, command.Status);

            _paymentRepository.Modify(payment);

            if (!await CommitAsync())
            {
                Notifications.Handle(PaymentCommandMessages.UpdateFailed);
                return result;
            }
            
            result.Success = true;
            result.Message = PaymentCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeletePaymentResult> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
        {
            var result = new DeletePaymentResult();
            
            var payment = await _paymentRepository.FindAsync(x => x.Id == command.Id);
            
            if (payment == null)
            {
                Notifications.Handle(PaymentCommandMessages.PaymentNotFound);
            }
            
            payment.Delete();
            
            _paymentRepository.Modify(payment);

            if (!await CommitAsync())
            {
                Notifications.Handle(PaymentCommandMessages.DeleteFailed);
                return result;
            }
            
            result.Success = true;
            result.Message = PaymentCommandMessages.DeleteSuccessful;
            
            return result;
        }
    }
}