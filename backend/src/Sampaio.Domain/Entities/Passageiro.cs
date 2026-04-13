using System;
using Newtonsoft.Json.Serialization;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Passageiro : BaseEntity
    {
        public Passageiro(
            Guid viagemId,
            int assento,
            string nome,
            string documento,
            string telefone,
            bool comprador,
            Guid? viagemPagamentoId = null)
        {
            Id = Guid.NewGuid();
            ViagemId = viagemId;
            Assento = assento;
            Nome = nome;
            Documento = documento;
            Telefone = telefone;
            Comprador = comprador;
            ViagemPagamentoId = viagemPagamentoId;
        }

        public Guid ViagemId { get; set; }

        public Viagem Viagem { get; set; }

        public Guid? ViagemPagamentoId { get; set; }

        public ViagemPagamento Pagamento { get; set; }

        public int Assento { get; set; }

        public string Nome { get; set; }

        public string Documento { get; set; }

        public string Telefone { get; set; }
        
        public bool Comprador { get; set; } = false;
    }
}