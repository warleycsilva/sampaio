using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Product;
using Sampaio.Shared.Enums;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Product
{
    public class UpdateProductCommand : IRequest<UpdateProductResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public EProductType? Type { get; set; }
        public Guid? CommerceId { get; set; }
        
        public Guid? ProductCategoryId { get; set; }
        
        public FileInput FileInput { get; set; }
        
        public string? ProductUrl { get; set; }
    }
}
