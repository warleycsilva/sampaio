using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.ProductCategory;


namespace Sampaio.Domain.Commands.ProductCategory
{
    public class DeleteProductCategoryCommand : IRequest<DeleteProductCategoryResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}