using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();

            builder.HasOne(x => x.Commerce)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.CommerceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Driver)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.DriverId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapEnumAsVarchar(x => x.Status, 100, true);

            builder.MapDecimal(x => x.Value, 18, 8, "SalePrice");
            
            builder.MapDecimal(x => x.DiscountValue, 18, 8);
            
        }
    }
}
