using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class DriverMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");
            
            builder.MapDeleted();

            builder.MapCreatedAt();
            
            builder.HasOne(x => x.User)
                .WithOne(x => x.Driver)
                .HasForeignKey<Driver>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.MapIdentification(x => x.Identification);
            
            builder.MapDateTime(x => x.Birthdate);
            
            builder.MapVarchar(x => x.CnhNumber, 20, false);

            builder.HasOne(x => x.Plan)
                .WithMany(x => x.Drivers)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.MapDateTime(x => x.PlanStartDate);
        }
    }
}