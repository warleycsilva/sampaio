using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Commerce
{
    public class CommercePagedListQuery: IRequest<PagedList<CommerceVm>>
    {
        public CommerceFilter Filter { get; set; }
    }
}