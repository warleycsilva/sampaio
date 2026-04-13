using System;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Extensions;

namespace Sampaio.Domain.ViewModels.Viagens
{
    public class PassageiroVm
    {
        public Guid Id { get; set; }
        
        public Guid ViagemId { get; set; }

        public Guid? ViagemPagamentoId { get; set; }

        public int Assento { get; set; }

        public string Nome { get; set; }

        public string Documento { get; set; }

        public string Telefone { get; set; }
        
        public bool Comprador { get; set; } = false;

        public string EmailComprador { get; set; }

        public string CpfComprador { get; set; }
        
        public DateTime? DataDaCompra { get; set; }
        
        public decimal? ValorPago { get; set; }
        
        public EPaymentStatus? StatusPagamento { get; set; }
        public string StatusPagamentoDescricao { get; set; } 
    }
}