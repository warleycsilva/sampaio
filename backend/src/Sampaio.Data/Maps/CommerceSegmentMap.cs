using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class CommerceSegmentMap : IEntityTypeConfiguration<CommerceSegment>
    {
        public void Configure(EntityTypeBuilder<CommerceSegment> builder)
        {
            builder.ToTable("CommerceSegments");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");
            
            builder.MapDeleted();

            builder.MapCreatedAt();

            builder.MapVarchar(x => x.Name, 255,true);
            
            builder.MapVarchar(x => x.Description, 2000,true);
        }
    }
}