using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Product;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Security;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Product
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public decimal DiscountPrice { get; set; }

        [Required]
        public EProductType Type { get; set; }
        
        
        public Guid? ProductCategoryId { get; set; }
        
        public string? ProductUrl { get; set; }
        
        public FileInput FileInput { get; set; }

        [JsonIgnore]
        public SessionUser? SessionUser { get; set; }
    }
}
