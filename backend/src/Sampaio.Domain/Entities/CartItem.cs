using System;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public Cart Cart { get; private set; }

        public Guid? CartId { get; private set; }

        public Product Product { get; private set; }

        public Guid? ProductId { get; private set; }

        public int Quantity { get; private set; }

        public static CartItem AddItem(Guid? cartId, Guid? productId, int quantity) => new CartItem()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            CartId = cartId,
            ProductId = productId,
            Quantity = quantity,
        };

        public void Update(Guid? productId, int? quantity)
        {
            this.ProductId = productId ?? ProductId;
            this.Quantity = quantity ?? Quantity;
        }

        public void UpQtd(int quantity)
        {
            this.Quantity = Quantity + quantity;
            Deleted = false;
        }

        public void Delete()
        {
            Deleted = true;
            Quantity = 0;
        }

        public void RemoveOne()
        {
            Quantity = Quantity - 1;
        }
    }
}