using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Queries.Establishment
{
    public class PagedListEstablishmentProductServicesQuery : IRequest<PagedList<ProductServiceVm>>
    {
        public ProductServiceFilter Filter { get; set; }

        [JsonIgnore] 
        public SessionUser SessionUser { get; set; }
    }
}