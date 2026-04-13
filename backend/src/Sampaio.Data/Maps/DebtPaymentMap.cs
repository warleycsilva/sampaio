using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class DebtPaymentMap : IEntityTypeConfiguration<DebtPayment>
    {
        public void Configure(EntityTypeBuilder<DebtPayment> builder)
        {
            builder.ToTable("DebtPayments");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();

            builder.HasOne(x => x.Debt)
                .WithMany(x => x.DebtPayments)
                .HasForeignKey(X => X.DebtId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapVarchar(x => x.ExternalCode, 255, false);

            builder.MapEnumAsVarchar(x => x.Status, 100, true);

        }
    }
}
