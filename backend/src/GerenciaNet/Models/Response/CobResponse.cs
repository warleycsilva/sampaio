using System;
using Newtonsoft.Json;

namespace GerenciaNet.Models.Response
{
    public class CobResponse
    {
        [JsonProperty(PropertyName = "calendario")]
        public Calendario Calendario { get; set; }

        [JsonProperty(PropertyName = "txid")]
        public string Txid { get; set; }

        [JsonProperty(PropertyName = "revisao")]
        public long Revisao { get; set; }

        [JsonProperty(PropertyName = "loc")]
        public Loc Loc { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "devedor")]
        public Devedor Devedor { get; set; }

        [JsonProperty(PropertyName = "valor")]
        public Valor Valor { get; set; }

        [JsonProperty(PropertyName = "chave")]
        public Guid Chave { get; set; }

        [JsonProperty(PropertyName = "solicitacaoPagador")]
        public string SolicitacaoPagador { get; set; }
    }

    public class Calendario
    {
        [JsonProperty(PropertyName = "criacao")]
        public DateTimeOffset Criacao { get; set; }

        [JsonProperty(PropertyName = "expiracao")]
        public long Expiracao { get; set; }
    }

    public class Devedor
    {
        [JsonProperty(PropertyName = "cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
    }

    public class Loc
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "tipoCob")]
        public string TipoCob { get; set; }
    }

    public class Valor
    {
        [JsonProperty(PropertyName = "original")]
        public string Original { get; set; }
    }
}