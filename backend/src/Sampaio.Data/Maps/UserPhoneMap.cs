using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    internal class UserPhoneMap : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.ToTable("UserPhones");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id);

            builder.MapUniqueIdentifier(x => x.UserId);

            builder.MapPhone(x => x.Phone, usePrefix: false);

            builder.MapCreatedAt();

            builder.MapDeleted();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Phones)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
