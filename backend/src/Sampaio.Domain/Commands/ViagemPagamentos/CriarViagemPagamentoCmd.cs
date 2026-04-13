using System;
using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Commands.Passageiros;
using Sampaio.Domain.Results.Viagens;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.ViagemPagamentos
{
    public class CriarViagemPagamentoCmd : IRequest<ViagemPagamentoBaseResult>
    {
        [JsonIgnore]
        public Guid ViagemId { get; set; }

        public ICollection<PassageiroCmd> Passageiros = new List<PassageiroCmd>();
        public EPaymentType TipoPagamento { get; set; } = EPaymentType.Pix;
        public string Email { get; set; }
        public string Cpf { get; set; }
        public CriarViagemPagamentoCartaoCreditoCmd? CartaoCredito { get; set; }
    }

    public class CriarViagemPagamentoCartaoCreditoCmd
    {
        public int? Parcelas { get; set; } = 1;
        public string NumeroCartao { get; set; }
        public string NomeTitularCartao { get; set; }
        public string DocTitularCartao { get; set; }
        public int MesExpiracao { get; set; }
        public int AnoExpiracao { get; set; }
        public string CodigoSegurancaCartao { get; set; }
        public string EnderecoCepCartao { get; set; }
        public string EnderecoCartao { get; set; }
        public string EnderecoNumeroCartao { get; set; }
        public string EnderecoRuaCartao { get; set; }
        public string EnderecoBairroCartao { get; set; }
        public string EnderecoCidadeCartao { get; set; }
        public string EnderecoEstadoCartao { get; set; }
    }
}