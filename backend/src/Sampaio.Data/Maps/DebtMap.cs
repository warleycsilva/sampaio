using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class DebtMap : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.ToTable("Debts");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();

            builder.HasOne(x => x.Driver)
                .WithMany(x => x.Debts)
                .HasForeignKey(x => x.DriverId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapDecimal(x => x.Value, 18, 2, "Value");

            builder.MapEnumAsVarchar(x => x.Status, 100, true);

            builder.MapEnumAsVarchar(x => x.Type, 100, true);
            
            builder.HasOne(x => x.Plan)
                .WithMany(x => x.Debts)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapVarcharMax(x => x.ExternalId, false);
            builder.MapVarcharMax(x => x.QrCode, false);
            builder.MapVarcharMax(x => x.QrCodeImage, false);
        }
    }
}
