using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.DebtPayment
{
    public class DebtPaymentListQuery : IRequest<PagedList<DebtPaymentVm>>
    {
        public DebtPaymentFilter Filter { get; set; }
    }
}