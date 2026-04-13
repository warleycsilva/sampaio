using System;
using System.Collections.Generic;

namespace Sampaio.Domain.ViewModels.Viagens
{
    public class ViagemVm
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public string Origem { get; set; }
        
        public string Observacoes { get; set; }

        public string Destino { get; set; }

        public DateTime DataPartida { get; set; }

        public decimal Preco { get; set; }

        public int QtdVagas { get; set; }
        public int VagasVendidas { get; set; }

        public ICollection<int> AssentosOcupados { get; set; } = new List<int>();
        public List<int> AssentosDisponiveis { get; set; } = new List<int>();
        
        public IEnumerable<ViagemPagamentoVm> ViagemPagamentos { get; set; } = new List<ViagemPagamentoVm>();
        
        public IEnumerable<PassageiroVm> Passageiros { get; set; } = new List<PassageiroVm>();
        
        public bool? IsActive { get; set; }

        public decimal ValorTotalFaturado { get; set; } = 0;
    }
}