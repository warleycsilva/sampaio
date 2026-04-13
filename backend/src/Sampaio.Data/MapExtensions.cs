using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Data
{
    internal static class MapExtensions
    {
        public static PropertyBuilder<Guid> MapUniqueIdentifier<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, Guid>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever()
                .IsRequired();
        }

        public static PropertyBuilder<string> MapVarchar<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, string>> exp,
            int size,
            bool isRequired,
            string columnName = null)
            where T : class
        {
            var b = builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType($"varchar({size})");
            return isRequired ? b.IsRequired() : b;
        }

        public static PropertyBuilder<Enum> MapEnumAsVarchar<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, Enum>> exp,
            int size,
            bool isRequired,
            string columnName = null)
            where T : class
        {
            var b = builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType($"varchar({size})")
                .HasConversion<string>();
            return isRequired ? b.IsRequired() : b;
        }

        public static PropertyBuilder<string> MapVarcharMax<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, string>> exp,
            bool isRequired,
            string columnName = null)
            where T : class
        {
            var b = builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("varchar(max)");
            return isRequired ? b.IsRequired() : b;
        }

        public static PropertyBuilder<Guid?> MapUniqueIdentifier<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, Guid?>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .ValueGeneratedNever()
                .HasColumnType("uniqueidentifier");
        }

        public static PropertyBuilder<bool> MapBit<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, bool>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("bit")
                .IsRequired();
        }

        public static PropertyBuilder<bool?> MapBit<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, bool?>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("bit");
        }

        public static PropertyBuilder<int> MapInt<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, int>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("int")
                .IsRequired();
        }
        
        public static PropertyBuilder<long> MapLong<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, long>> exp,
            string columnName)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName)
                .HasColumnType("bigint")
                .IsRequired(true);
        }

        public static PropertyBuilder<int?> MapInt<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, int?>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("int");
        }

        public static PropertyBuilder<DateTime> MapDateTime<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, DateTime>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("datetime")
                .IsRequired();
        }

        public static PropertyBuilder<DateTime?> MapDateTime<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, DateTime?>> exp,
            string columnName = null)
            where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName ?? exp.Name)
                .HasColumnType("datetime");
        }

        public static PropertyBuilder<decimal> MapDecimal<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, decimal>> exp,
            int range,
            int precision,
            string columnName = null) where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName)
                .HasColumnType($"decimal({range},{precision})")
                .IsRequired();
                
        }
        
        public static PropertyBuilder<decimal?> MapDecimal<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, decimal?>> exp,
            int range,
            int precision,
            string columnName = null) where T : class
        {
            return builder.Property(exp)
                .HasColumnName(columnName)
                .HasColumnType($"decimal({range},{precision})");

        }
        

    public static PropertyBuilder<bool> MapDeleted<T>(this EntityTypeBuilder<T> builder)
            where T : BaseEntity
        {
            return builder.Property(x => x.Deleted)
                .HasColumnName("Deleted")
                .HasColumnType("bit")
                .IsRequired();
        }
        
        public static PropertyBuilder<DateTime> MapCreatedAt<T>(this EntityTypeBuilder<T> builder)
            where T : BaseEntity
        {
            return builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime")
                .IsRequired();
        }

        public static EntityTypeBuilder<T> MapIdentification<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, Identification>> exp, string prefix = "Identification", bool usePrefix = true)
            where T : class
        {
            prefix = usePrefix ? $"{prefix}_"  : "";
            
            return builder.OwnsOne(exp, b =>
            {
                b.WithOwner();
                
                b.Property(x => x.Number)
                    .HasColumnType("varchar(100)")
                    .HasColumnName($"{prefix}Number");

                b.Property(x => x.Type)
                    .HasColumnName($"{prefix}Type")
                    .HasConversion<string>()
                    .HasColumnType("varchar(100)");
            });
        }
        
        public static EntityTypeBuilder<T> MapAddressInformation<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, AddressInformation>> exp, string prefix = "AddressInformation", bool usePrefix = true)
            where T : class
        {
            prefix = usePrefix ? $"{prefix}_"  : "";
            
            return builder.OwnsOne(exp, b =>
            {
                b.Property(x => x.Address)
                    .HasColumnType("varchar(255)")
                    .HasColumnName($"{prefix}Address");
                
                b.Property(x => x.Number)
                    .HasColumnType("varchar(100)")
                    .HasColumnName($"{prefix}Number");

                b.Property(x => x.Complement)
                    .HasColumnType("varchar(max)")
                    .HasColumnName($"{prefix}Complement");
                
                b.Property(x => x.Neighborhood)
                    .HasColumnType("varchar(100)")
                    .HasColumnName($"{prefix}Neighborhood");
                
                b.Property(x => x.ZipCode)
                    .HasColumnType("varchar(100)")
                    .HasColumnName($"{prefix}ZipCode");
                
                // b.Property(x => x.City)
                //     .HasColumnType("varchar(100)")
                //     .HasColumnName($"{prefix}City");
                //
                // b.Property(x => x.State)
                //     .HasColumnType("varchar(100)")
                //     .HasColumnName($"{prefix}State");
                
                b.HasOne(x => x.State)
                    .WithMany()
                    .HasForeignKey(x => x.StateId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(x => x.City)
                    .WithMany()
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        public static EntityTypeBuilder<T> MapPhone<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, Phone>> exp, string prefix = "Phone", bool usePrefix = true)
            where T : class
        {
            prefix = usePrefix ? $"{prefix}_"  : "";
            
            return builder.OwnsOne(exp, b =>
            {
                b.Property(x => x.Number)
                    .HasColumnType("varchar(50)")
                    .HasColumnName($"{prefix}Number");
                
                b.Property(x => x.Type)
                    .HasColumnName($"{prefix}Type")
                    .HasConversion<string>()
                    .HasColumnType("varchar(100)");
            });
        }
    }
}
