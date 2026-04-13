using System;
using System.Collections.Generic;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public decimal DiscountPrice { get; private set; }

        public Commerce Commerce { get; private set; }
        public Guid CommerceId { get; private set; }

        public EProductType Type { get; private set; }
        
        public ProductCategory ProductCategory { get; private set; }
        
        public Guid? ProductCategoryId { get; private set; }
        
        public string? ProductUrl { get; private set; }

        public ICollection<SaleItem> SaleItems { get; private set; } = new List<SaleItem>();
        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();

        public static Product New(string name, string description, decimal price, decimal discountPrice,
            Guid commerceId, EProductType type, Guid? productCategoryId, string? productUrl) => new Product()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            Name = name,
            Description = description,
            Price = price ,
            DiscountPrice = discountPrice,
            CommerceId = commerceId,
            ProductCategoryId = productCategoryId,
            ProductUrl = productUrl
        };

        public void Update(string? name, string? description, decimal? price, decimal? discountPrice, Guid? commerceId,
            EProductType? type, Guid? productCategoryId, string? productUrl, FileInput b)
        {
            this.Name = name;
            this.Description = description ?? Description;
            this.Price = price != null ? price.Value  : Price;
            this.DiscountPrice = discountPrice != null ? discountPrice.Value  : DiscountPrice;
            this.CommerceId = commerceId ?? CommerceId;
            this.Type = type ?? Type;
            this.ProductCategoryId = productCategoryId ?? ProductCategoryId;
            this.ProductUrl = productUrl ?? ProductUrl;

        }

        public void Delete() => Deleted = true;

        public void SetCategory(Guid productCategoryId)
        {
            ProductCategoryId = productCategoryId;
        }
        
        public void UpdateProductUrl(string productUrl) => ProductUrl = productUrl;
    }
}