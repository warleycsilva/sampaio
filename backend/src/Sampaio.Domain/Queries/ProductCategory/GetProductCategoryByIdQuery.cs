using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.ProductCategory
{
    public class GetProductCategoryByIdQuery : IRequest<ProductCategoryVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}