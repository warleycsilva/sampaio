using MediatR;
using Newtonsoft.Json;
using Sampaio.Shared.Security;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.ProductService
{
    public class ListProductServiceQuery : IRequest<PagedList<ProductServiceVm>>
    {
        public ProductServiceFilter Filter { get; set; }
        
    }
}