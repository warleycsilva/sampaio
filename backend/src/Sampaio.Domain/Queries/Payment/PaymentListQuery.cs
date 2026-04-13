using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Payment
{
    public class PaymentListQuery : IRequest<PagedList<PaymentVm>>
    {
        public PaymentFilter Filter { get; set; }
    }
}