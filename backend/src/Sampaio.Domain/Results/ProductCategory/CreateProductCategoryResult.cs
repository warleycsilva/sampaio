using System;

namespace Sampaio.Domain.Results.ProductCategory
{
    public class CreateProductCategoryResult
    {
        public Guid ProductCategoryId { get; set; }
        
        public bool Success { get; set; } = false;
        
        public string Message { get; set;}
    }
}