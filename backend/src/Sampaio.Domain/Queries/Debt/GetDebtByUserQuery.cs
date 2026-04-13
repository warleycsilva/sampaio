using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Debt
{
    public class GetDebtByUserQuery : IRequest<PagedList<DebtVm>>
    {
        public DebtFilter Filter { get; set; }
    }
}