using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapDeleted();

            builder.MapCreatedAt();

            builder.HasOne(x => x.Sale)
                .WithMany(x => x.SaleItems)
                .HasForeignKey(x => x.SaleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.SaleItems)
                .HasForeignKey(x => x.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapInt(x => x.Quantity);
            
            builder.MapVarchar(x => x.ProductName, 255, false,"ProductName");
            builder.MapDecimal(x => x.ProductPrice, 18, 2, "ProductPrice");

            builder.MapDecimal(x => x.ProductDiscountPrice, 18, 2, "ProductDiscountPrice");
        }
    }
}