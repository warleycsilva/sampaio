using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            
            builder.HasKey(x => x.Id);
            
            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();
            
            builder.MapDeleted();

            builder.HasOne(x => x.Debt)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.DebtId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapEnumAsVarchar(x => x.Status, 100, true);
        }
    }
}