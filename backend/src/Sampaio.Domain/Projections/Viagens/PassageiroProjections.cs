using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels.Viagens;

namespace Sampaio.Domain.Projections.Viagens
{
    public static class PassageiroProjections
    {
        public static PassageiroVm ToVm(this Passageiro i) => new PassageiroVm
        {
            Id = i.Id,
            Assento = i.Assento,
            Comprador = i.Comprador,
            Documento = i.Documento?.ToString(),
            Nome = i.Nome,
            Telefone = i.Telefone,
            ViagemId = i.ViagemId,
            ViagemPagamentoId = i.ViagemPagamentoId,
        };
        
        public static IEnumerable<PassageiroVm> ToVm(this ICollection<Passageiro> query) =>
            query.Select(i => i.ToVm());
        
        public static IQueryable<PassageiroVm> ToVm(this IQueryable<Passageiro> query) =>
            query.Select(i => i != null ? i.ToVm() : null);
    }
}