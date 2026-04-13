using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Projections.Viagens
{
    public static class ViagemProjections
    {
        public static ViagemVm ToVm(this Viagem i) => new ViagemVm
        {
            Id = i.Id,
            IsActive = i.IsActive,
            CreatedAt = i.CreatedAt,
            Destino = i.Destino,
            Origem = i.Origem,
            Preco = i.Preco,
            DataPartida = i.DataPartida,
            QtdVagas = i.QtdVagas,
            ViagemPagamentos = i.ViagemPagamentos != null ? i.ViagemPagamentos.ToVm() : null,
            Passageiros = i.Passageiros != null ? i.Passageiros.ToVm() : null,
            Observacoes = i.Observacoes
        };
        
        public static IEnumerable<ViagemVm> ToVm(this ICollection<Viagem> query) =>
            query.Select(v => v.ToVm());

        public static IQueryable<ViagemVm> ToVm(this IQueryable<Viagem> query) =>
            query.Select(i => i != null ? i.ToVm() : null );
    }
}