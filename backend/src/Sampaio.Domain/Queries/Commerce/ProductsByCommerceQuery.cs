using System;
using System.Text.Json.Serialization;
using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Commerce
{
    public class ProductsByCommerceQuery : IRequest<PagedList<ProductVm>>
    {
        public ProductFilter Filter { get; set; }
    }
}