using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class PassageiroMap : IEntityTypeConfiguration<Passageiro>
    {
        public void Configure(EntityTypeBuilder<Passageiro> builder)
        {
            builder.ToTable("Passageiros");
            
            builder.HasKey(x => x.Id);
            
            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();
            
            builder.HasOne(x => x.Viagem)
                .WithMany(x => x.Passageiros)
                .HasForeignKey(x => x.ViagemId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(x => x.Pagamento)
                .WithMany(x => x.Passageiros)
                .HasForeignKey(x => x.ViagemPagamentoId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.MapInt(x => x.Assento);

            builder.MapVarchar(x => x.Nome, 1000, true);

            builder.MapVarchar(x => x.Documento, 500, true);

            builder.MapVarchar(x => x.Telefone, 500, true);

            builder.MapBit(x => x.Comprador);
        }
    }
}