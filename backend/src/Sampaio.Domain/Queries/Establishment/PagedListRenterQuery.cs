using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Establishment
{
    public class PagedListEstablishmentQuery : IRequest<PagedList<EstablishmentVm>>
    {
        public EstablishmentFilter Filter { get; set; }
    }
}
