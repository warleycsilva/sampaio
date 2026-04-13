using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public decimal Value { get; private set; } // ver se poode ser Price

        public decimal? DiscountValue { get; private set; }    
        public Commerce Commerce { get; private set; }
        public Guid? CommerceId { get; private set; }

        public Driver Driver { get; private set; }
        public Guid? DriverId { get; private set; }

        public ESalesStatus Status { get; private set; }

        public ICollection<SaleItem> SaleItems { get; private set; } = new List<SaleItem>();
        
        public static Sale New(decimal value,
            decimal? discountValue, 
            Guid? commerceId, Guid? driverId) => new Sale()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            Value = value,
            DiscountValue = discountValue,
            CommerceId = commerceId,
            DriverId = driverId,
            Status = ESalesStatus.New,
        };

        public void AddCartItem(CartItem item, string productName,decimal  productPrice,decimal  productDiscount)
        {
            SaleItems.Add(SaleItem.New(Id, item.ProductId, item.Quantity, productName, productPrice,productDiscount));
        }

        public void Update(decimal? value, Guid? commerceId, Guid? driverId, ESalesStatus? status, decimal? discountValue)
        {
            this.Value = value ?? Value;
            this.CommerceId = commerceId ?? CommerceId;
            this.DriverId = driverId ?? DriverId;
            this.Status = status ?? Status;
            this.DiscountValue = discountValue ?? DiscountValue;

        }

        public void Delete() => Deleted = true;
        public void SetPrice(decimal value) => Value = value;

        public void AbandonCart(decimal? value, Guid? commerceId, Guid? driverId)
        {
            this.Value = value ?? Value;
            this.CommerceId = commerceId ?? CommerceId;
            this.DriverId = driverId ?? DriverId;
        }

        public void Complete()
        {
            Status = ESalesStatus.Complete;
        }

        public void Finalize()
        {
            Status = ESalesStatus.Finalized;
        }
    }
}
