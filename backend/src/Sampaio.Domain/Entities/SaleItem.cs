using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Sale Sale { get; private set; }
        public Guid? SaleId { get; private set; }

        public Product Product { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public decimal ProductDiscountPrice { get; private set; }
        public Guid? ProductId { get; private set; }

        public int Quantity { get; private set; }

        public static SaleItem New(Guid? saleId, Guid? productId, int quantity, string productName, decimal productPrice, decimal productDiscountPrice) => new SaleItem()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SaleId = saleId,
            ProductId = productId,
            Quantity = quantity,
            ProductName = productName,
            ProductPrice = productPrice,
            ProductDiscountPrice = productDiscountPrice
        };

        public void Update(Guid? saleId, Guid? productId, int? quantity)
        {
            this.SaleId = saleId ?? SaleId;
            this.ProductId = productId ?? ProductId;
            this.Quantity = quantity ?? Quantity;
        }

        public void Delete() => Deleted = true;
    }
}