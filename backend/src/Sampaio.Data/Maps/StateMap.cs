using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");

            builder.HasKey(x => x.Initials);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("Varchar(255)")
                .IsRequired();

            builder.Property(x => x.Initials)
                .HasColumnName("Initials")
                .HasColumnType("Varchar(2)")
                .IsRequired();
        }
    }
}