using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using MediatR;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.Payment;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class PaymentQueryHandler : BaseQueryHandler,
        IRequestHandler<GetPaymentByIdQuery, PaymentVm>,
        IRequestHandler<PaymentListQuery, PagedList<PaymentVm>>
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentQueryHandler(IDomainNotification notifications,
            IPaymentRepository paymentRepository)
            : base(notifications)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentVm> Handle(GetPaymentByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Payment>()
                .Include(x => x.Debt.Driver.User.Phones)
                .Includes;
            
            var payment = await _paymentRepository.FindAsync(x => x.Id == query.Id, includes);

            if (payment == null)
            {
                Notifications.Handle(PaymentCommandMessage.PaymentNotFound);
                return null;
            }
            var result = payment.ToVm();
            
            return result;
        }

        public async Task<PagedList<PaymentVm>> Handle(PaymentListQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Payment>()
                .Include(x => x.Debt.Driver.User.Phones)
                .Includes;
            
            var where = _paymentRepository.Where(query.Filter);
            
            var count = await _paymentRepository.CountAsync(where);

            if (count == 0)
            {
                Notifications.Handle(PaymentCommandMessage.PaymentNotFound);
                return null;
            }
            
            var payment = _paymentRepository.ListAsNoTracking(where, query.Filter, includes).ToVm();

            return new PagedList<PaymentVm>(payment, count, query.Filter.PageSize);
        }
    }
}