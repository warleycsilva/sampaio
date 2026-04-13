using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.ProductCategory;

namespace Sampaio.Domain.Commands.ProductCategory
{
    public class CreateProductCategoryCommand : IRequest<CreateProductCategoryResult>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}