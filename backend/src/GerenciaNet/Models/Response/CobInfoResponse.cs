using System;
using Newtonsoft.Json;

namespace GerenciaNet.Models.Response
{
    public class CobInfoResponse
    {
         [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("calendario")]
        public Calendario Calendario { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("txid")]
        public Guid Txid { get; set; }

        [JsonProperty("revisao")]
        public long Revisao { get; set; }

        [JsonProperty("devedor")]
        public Dor Devedor { get; set; }

        [JsonProperty("valor")]
        public Valor Valor { get; set; }

        [JsonProperty("chave")]
        public Guid Chave { get; set; }

        [JsonProperty("solicitacaoPagador")]
        public string SolicitacaoPagador { get; set; }

        [JsonProperty("pix")]
        public Pix[] Pix { get; set; }
        
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public partial class Dor
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public partial class Pix
    {
        [JsonProperty("endToEndId")]
        public string EndToEndId { get; set; }

        [JsonProperty("txid")]
        public Guid Txid { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }

        [JsonProperty("horario")]
        public DateTimeOffset Horario { get; set; }

        [JsonProperty("pagador")]
        public Dor Pagador { get; set; }

        [JsonProperty("infoPagador")]
        public string InfoPagador { get; set; }

        [JsonProperty("devolucoes")]
        public Devolucoe[] Devolucoes { get; set; }
    }

    public partial class Devolucoe
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rtrId")]
        public string RtrId { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }

        [JsonProperty("horario")]
        public Horario Horario { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Horario
    {
        [JsonProperty("solicitacao")]
        public DateTimeOffset Solicitacao { get; set; }
    }
}