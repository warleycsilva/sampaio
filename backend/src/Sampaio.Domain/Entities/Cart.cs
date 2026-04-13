using System;
using System.Collections.Generic;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Commerce Commerce { get; private set; }

        public Guid? CommerceId { get; private set; }

        public Driver Driver { get; private set; }

        public Guid? DriverId { get; private set; }

        public decimal Value { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public static Cart New(Guid? commerceId, Guid? driverId) => new Cart()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            CommerceId = commerceId,
            DriverId = driverId,
        };


        public void AddCommerceId(Guid commerceId)
        {
            CommerceId = commerceId;
        }

        public void Update(Guid? commerceId, Guid? driverId, decimal? value)
        {
            this.CommerceId = commerceId ?? CommerceId;
            this.DriverId = driverId ?? DriverId;
            this.Value = value ?? Value;
        }

        public void Delete() => Deleted = true;

        public void AddToSubTotal(int qty, decimal discountValue, decimal value)
        {
            DiscountValue += qty * discountValue;
            Value += qty * value;
        }

        public void UpdateValue(decimal value, decimal? discountValue = 0)
        {
            Value = value;
            DiscountValue = discountValue;
        }

        public void AbandonCart() => Deleted = true;

        public Sale Finalize()
        {
            return Sale.New(Value, DiscountValue, CommerceId, DriverId);
        }

    }
}