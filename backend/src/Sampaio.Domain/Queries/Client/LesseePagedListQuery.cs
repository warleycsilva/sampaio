using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Client
{
    public class LesseePagedListQuery: IRequest<PagedList<ClientVm>>
    {
        public ClientFilter Filter { get; set; }
    }
}