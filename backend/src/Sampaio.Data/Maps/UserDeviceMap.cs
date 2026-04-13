using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    internal class UserDeviceMap : IEntityTypeConfiguration<UserDevice>
    {
        public void Configure(EntityTypeBuilder<UserDevice> builder)
        {
            builder.ToTable("UserDevices");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");
            
            builder.MapVarcharMax(x => x.DeviceId, true);
            builder.MapVarcharMax(x => x.DeviceToken, true);

            builder.MapDeleted();

            builder.MapCreatedAt();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserDevices)
                .HasForeignKey(x => x.UserId);
        }
    }
}