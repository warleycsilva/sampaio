using System;
using System.Collections.Generic;
using Sampaio.Domain.Entities;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels.Viagens
{
    public class ViagemPagamentoVm
    {
        public Guid Id { get; set; }
        
        public Guid ViagemId { get; set; }
        
        public int QuantidadePassagens { get; set; }
        
        public decimal ValorTotal { get; set; }
        
        public string IdTransacao { get; set; }

        public EPaymentStatus PagamentoStatus { get; set; } = EPaymentStatus.Pending;
        
        public IEnumerable<PassageiroVm> Passageiros { get; set; } = new List<PassageiroVm>();
        
        public string Email { get; set; }
        
        public string Cpf { get; set; }
        
        public DateTime DataDaCompra { get; set; }
    }
}