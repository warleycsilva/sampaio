using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.ProductCategory;

namespace Sampaio.Domain.Commands.ProductCategory
{
    public class UpdateProductCategoryCommand : IRequest<UpdateProductCategoryResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }
    }
}