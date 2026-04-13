using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();

            builder.HasOne(x => x.Commerce)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CommerceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapEnumAsVarchar(x => x.Type, 100, true);

            builder.MapVarchar(x => x.Name, 255, false);

            builder.MapVarchar(x => x.Description, 255, false);

            builder.MapDecimal(x => x.Price, 18, 2, "OriginalPrice");

            builder.MapDecimal(x => x.DiscountPrice, 18, 2, "Discount");

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapVarchar(x => x.ProductUrl, 5000, false);

        }
    }
}