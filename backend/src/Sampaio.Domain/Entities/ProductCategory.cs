using System;
using System.Collections.Generic;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        protected ProductCategory()
        {
        }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public string ProductUrl { get; private set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public static ProductCategory New(string name, string description)
        {
            return new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };
        }

        public ProductCategory Update(string? name, string? description)
        {
            Name = name;
            Description = description;
            return this;
        }

        public void Delete() => Deleted = true;
        
        public void UpdateProductUrl(string productUrl) => ProductUrl = productUrl;

    }
}