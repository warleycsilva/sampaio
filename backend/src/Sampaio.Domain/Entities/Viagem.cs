using System;
using System.Collections.Generic;
using Sampaio.Domain.Commands.Viagens;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Viagem : BaseEntity
    {
        public Viagem(
            string origem,
            string destino,
            DateTime dataPartida,
            decimal preco,
            int qtdVagas,
            string observacoes)
        {
            Id = Guid.NewGuid();
            Origem = origem;
            Destino = destino;
            DataPartida = dataPartida;
            Preco = preco;
            QtdVagas = qtdVagas;
            Observacoes = observacoes;
        }

        public void Update(EditarViagemCmd cmd)
        {
            Origem = cmd.Origem;
            Destino = cmd.Destino;
            DataPartida = cmd.DataPartida;
            Preco = cmd.Preco;
            QtdVagas = cmd.QtdVagas;
            Observacoes = cmd.Observacoes;
        }

        public string Origem { get; set; }

        public string Destino { get; set; }
        
        public string Observacoes { get; set; }

        public DateTime DataPartida { get; set; }

        public decimal Preco { get; set; }

        public int QtdVagas { get; set; }
        
        public ICollection<ViagemPagamento> ViagemPagamentos { get; set; } = new List<ViagemPagamento>();
        
        public ICollection<Passageiro> Passageiros { get; set; }= new List<Passageiro>();
        
        public bool? IsActive { get; set; } = true;
        
        public void DesativarViagem() => IsActive = false;
        public void AtivarViagem() => IsActive = true;
        public void ToggleActiveStatus() => IsActive = !IsActive;
    }
}