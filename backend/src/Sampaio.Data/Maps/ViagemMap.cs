using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class ViagemMap : IEntityTypeConfiguration<Viagem>
    {
        public void Configure(EntityTypeBuilder<Viagem> builder)
        {
            builder.ToTable("Viagens");
            
            builder.HasKey(x => x.Id);
            
            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();

            builder.MapVarchar(x => x.Origem, 5000, true);

            builder.MapVarchar(x => x.Destino, 5000, true);
            
            builder.MapVarcharMax(x => x.Observacoes, false);

            builder.MapDateTime(x => x.DataPartida);

            builder.MapDecimal(x => x.Preco, 18, 2);
            
            builder.MapInt(x => x.QtdVagas);
            
            builder.MapBit(x => x.IsActive, "is_active");
        }
    }
}