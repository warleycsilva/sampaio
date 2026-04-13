using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class ViagemPagamentoMap : IEntityTypeConfiguration<ViagemPagamento>
    {
        public void Configure(EntityTypeBuilder<ViagemPagamento> builder)
        {
            builder.ToTable("ViagemPagamentos");
            
            builder.HasKey(x => x.Id);
            
            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapCreatedAt();

            builder.MapDeleted();
            
            builder.HasOne(x => x.Viagem)
                .WithMany(x => x.ViagemPagamentos)
                .HasForeignKey(x => x.ViagemId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.MapInt(x => x.QuantidadePassagens);

            builder.MapDecimal(x => x.ValorTotal, 18, 2);

            builder.MapVarchar(x => x.IdTransacao, 500, false);
            
            builder.MapVarchar(x => x.PixQrCode, 1000, false);

            builder.MapEnumAsVarchar(x => x.PagamentoStatus, 500, true);
            
            builder.MapVarchar(x => x.Email, 255, true, "email");
            
            builder.MapVarchar(x => x.Cpf, 11,true,"cpf");
        }
    }
}