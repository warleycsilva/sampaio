using System.Collections.Generic;
using Sampaio.Shared.Paging;
using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.ProductCategory
{
    public class ProductCategoryListQuery : IRequest<IEnumerable<ProductCategoryVm>>
    {
        public ProductCategoryFilter Filter { get; set; }
    }
}