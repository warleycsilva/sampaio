using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Product
{
    public class ProductListQuery : IRequest<PagedList<ProductVm>>
    {
        public ProductFilter Filter { get; set; }
    }
}
