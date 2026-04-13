using System;
using System.Collections.Generic;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class ViagemPagamento : BaseEntity
    {
        public ViagemPagamento(
            Guid viagemId,
            int quantidadePassagens,
            decimal valorTotal,
            string email,
            string cpf,
            string? idTransacao = null,
            EPaymentStatus pagamentoStatus = EPaymentStatus.Pending
            )
        {
            Id = Guid.NewGuid();
            ViagemId = viagemId;
            QuantidadePassagens = quantidadePassagens;
            ValorTotal = valorTotal;
            IdTransacao = idTransacao;
            PagamentoStatus = pagamentoStatus;
            Email = email;
            Cpf = cpf;
        }

        public Guid ViagemId { get; set; }

        public Viagem Viagem { get; set; }
        
        public int QuantidadePassagens { get; set; }
        
        public decimal ValorTotal { get; set; }
        
        public string IdTransacao { get; set; }
        
        public string PixQrCode { get; set; }

        public EPaymentStatus PagamentoStatus { get; set; } = EPaymentStatus.Pending;
        
        public ICollection<Passageiro> Passageiros { get; set; } = new List<Passageiro>();
        
        public string Email { get; set; }
        
        public string Cpf { get; set; }

        public ViagemPagamento AtualizarStatus(string orderStatus, string? pixQrCode = null)
        {
            PagamentoStatus = orderStatus switch
            {
                "paid" => EPaymentStatus.Approved,
                "pending" => EPaymentStatus.Pending,
                "canceled" => EPaymentStatus.Canceled,
                "failed" => EPaymentStatus.Failed,
                "refunded" => EPaymentStatus.Refunded,
                _ => PagamentoStatus
            };
            PixQrCode = pixQrCode ?? PixQrCode;
            return this;
        }
        
        public void ReembolsarPagamento()
        {
            PagamentoStatus = EPaymentStatus.Refunded;
        }
    }
}