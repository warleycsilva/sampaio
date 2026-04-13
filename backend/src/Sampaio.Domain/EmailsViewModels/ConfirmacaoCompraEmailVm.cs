using System;
using System.Collections.Generic;
using Sampaio.Shared.Structs;

namespace Sampaio.Domain.EmailsViewModels
{
    public class ConfirmacaoCompraEmailVm
    {
        public ConfirmacaoCompraEmailVm()
        {
            
        }

        public string Nome { get; set; }

        public string Email { get; set; }
        
        public string Cpf { get; set; }
        
        public decimal ValorTotal { get; set; }
        
        public string Origem { get; set; }
        
        public string Destino { get; set; }
        
        public DateTime DataPartida { get; set; }
        public string FormaPagamento { get; set; } = "Pix";
        
        public string Assunto { get; set; }

        public ICollection<PassageiroConfirmacaoAssentoEmailVm> Passageiros { get; set; } =
            new List<PassageiroConfirmacaoAssentoEmailVm>();

        public string Observacoes { get; set; }
    }

    public class PassageiroConfirmacaoAssentoEmailVm
    {
        public int Assento { get; set; }
        
        public string Nome { get; set; }
        
        public string Documento { get; set; }
    }
    
}