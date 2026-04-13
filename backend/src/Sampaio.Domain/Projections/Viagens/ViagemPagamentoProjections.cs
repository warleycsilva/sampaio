using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels.Viagens;

namespace Sampaio.Domain.Projections.Viagens
{
    public static class ViagemPagamentoPagamentoProjections
    {
        public static ViagemPagamentoVm ToVm(this ViagemPagamento v) => new ViagemPagamentoVm
        {
            Id = v.Id,
            IdTransacao = v.IdTransacao,
            PagamentoStatus = v.PagamentoStatus,
            QuantidadePassagens = v.QuantidadePassagens,
            ValorTotal = v.ValorTotal,
            ViagemId = v.ViagemId,
            Passageiros = v.Passageiros != null ? v.Passageiros.ToVm() : null,
            Email = v.Email,
            Cpf = v.Cpf?.ToString(), 
            DataDaCompra = v.CreatedAt, 
        };
        public static IEnumerable<ViagemPagamentoVm> ToVm(this ICollection<ViagemPagamento> query) =>
            query.Select(v => v.ToVm());
        
        public static IQueryable<ViagemPagamentoVm> ToVm(this IQueryable<ViagemPagamento> query) =>
            query.Select(v => v != null ? v.ToVm() : null);
    }
}