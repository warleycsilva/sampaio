using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using MediatR;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.DebtPayment;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class DebtPaymentQueryHandler : BaseQueryHandler,
        IRequestHandler<GetDebtPaymentByIdQuery, DebtPaymentVm>,
        IRequestHandler<DebtPaymentListQuery, PagedList<DebtPaymentVm>>
    {
        private readonly IDebtPaymentRepository _debtPaymentRepository;
        
        public DebtPaymentQueryHandler(IDomainNotification notifications,
            IDebtPaymentRepository debtPaymentRepository) : base(notifications)
        {
            _debtPaymentRepository = debtPaymentRepository;
        }

        public async Task<DebtPaymentVm> Handle(GetDebtPaymentByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<DebtPayment>()
                .Include(x => x.Debt.Driver.User.Phones)
                .Includes;
            
            var debtPayment = await _debtPaymentRepository.FindAsync(x => x.Id == query.Id, includes);
            
            var result = debtPayment.ToVm();
            
            return result;
        }

        public async Task<PagedList<DebtPaymentVm>> Handle(DebtPaymentListQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<DebtPayment>()
                .Include(x => x.Debt.Driver.User.Phones)
                .Includes;

            var where = _debtPaymentRepository.Where(query.Filter);
            
            var count = await _debtPaymentRepository.CountAsync(where);
            
            var debtPayment = _debtPaymentRepository.ListAsNoTracking(where, query.Filter, includes).ToVm();

            return new PagedList<DebtPaymentVm>(debtPayment, count, query.Filter.PageSize);
        }
    }
}