using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class CartMap : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            
            builder.HasKey(x => x.Id);
            
            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();
            
            builder.HasOne(x => x.Commerce)
                .WithMany(x => x.Carts)
                .HasForeignKey(x => x.CommerceId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(x => x.Driver)
                .WithMany(x => x.Carts)
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapDecimal(x => x.Value, 18, 2, "Price");
            
            builder.MapDecimal(x => x.DiscountValue, 18, 2);
        }
    }
}