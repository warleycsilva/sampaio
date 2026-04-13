using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    internal class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapDateTime(x => x.OccurredAt, "OccurredAt");

            builder.MapVarchar(x => x.Level,  255, false);
            
            builder.MapVarchar(x => x.Logger, 255, false);
            
            builder.MapVarcharMax(x => x.Message,  false);
            
            builder.MapVarcharMax(x => x.Exception,  false);
        }
    }
}
