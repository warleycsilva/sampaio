using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class CommerceMap : IEntityTypeConfiguration<Commerce>
    {
        public void Configure(EntityTypeBuilder<Commerce> builder)
        {
            builder.ToTable("Commerces");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");
            
            builder.MapDeleted();

            builder.MapCreatedAt();
            
            builder.HasOne(x => x.User)
                .WithOne(x => x.Commerce)
                .HasForeignKey<Commerce>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.MapIdentification(x => x.Identification);
            
            
            builder.MapAddressInformation(x => x.AddressInformation);
            
            builder.MapVarchar(x => x.Name, 5000, false);
            
            builder.HasOne(x => x.Segment)
                .WithMany(x => x.Commerces)
                .HasForeignKey(x => x.SegmentId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.MapBit(x => x.HasParking);
            
            builder.MapBit(x => x.Homologated);
            
        }
    }
}