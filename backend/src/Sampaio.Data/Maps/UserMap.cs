using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.HasKey(x => x.Id);
            
            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapBit(x => x.Active, "Active");

            builder.MapVarchar(x => x.Email, 255, true);

            builder.MapVarchar(x => x.Password, 5000, true);

            builder.MapVarchar(x => x.FirstName, 255, true);

            builder.MapVarchar(x => x.LastName, 255, false);

            builder.MapVarchar(x => x.AvatarUrl, 5000, false);

            builder.MapVarcharMax(x => x.PasswordRecoverToken, false);
            
            builder.MapVarcharMax(x => x.ActivationToken, false);
            
            builder.MapDeleted();
            
            builder.MapCreatedAt();

            builder.HasIndex(x => x.Email)
                .IsUnique(true);
        }
    }
}
