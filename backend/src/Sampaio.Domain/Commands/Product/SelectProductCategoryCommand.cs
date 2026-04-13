using System;
using System.ComponentModel.DataAnnotations;
using Sampaio.Shared.Security;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Product;

namespace Sampaio.Domain.Commands.Product
{
    public class SelectProductCategoryCommand : IRequest<SelectProductCategoryResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductCategoryId { get; set; }
    }
}