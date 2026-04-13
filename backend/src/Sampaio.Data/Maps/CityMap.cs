using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            
            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");
            
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("Varchar(Max)")
                .IsRequired();

            builder.HasOne(x => x.State)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}