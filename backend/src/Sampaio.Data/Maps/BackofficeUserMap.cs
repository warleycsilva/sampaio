using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class BackofficeUserMap : IEntityTypeConfiguration<BackofficeUser>
    {
        public void Configure(EntityTypeBuilder<BackofficeUser> builder)
        {
            builder.ToTable("BackofficeUsers");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapDeleted();

            builder.MapCreatedAt();

            builder.HasOne(x => x.User)
                .WithOne(x => x.BackofficeUser)
                .HasForeignKey<BackofficeUser>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
