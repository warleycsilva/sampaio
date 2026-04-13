using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace GerenciaNet.Models.Request
{
    public class CobRequest
    {
        [JsonProperty(PropertyName = "body")]
        public PixBody Body { get; set; }
    }
    public class PixBody
    {
        [JsonProperty(PropertyName = "calendario")]
        public Calendar Calendar { get; set; }   
        [JsonProperty(PropertyName = "devedor")]
        public Debtor Debtor { get; set; }   
        [JsonProperty(PropertyName = "valor")]
        public Value Value { get; set; }   
        [JsonProperty(PropertyName = "chave")]
        public string Key { get; set; }   
        [JsonProperty(PropertyName = "solicitacaoPagador")]
        public string RequestPayer { get; set; }   
    }
    public class Calendar
    {
        [JsonProperty(PropertyName = "expiracao")]
        public int Expiration { get; set; }   
    }
    public class Debtor
    {
        [JsonProperty(PropertyName = "cnpj")] public string Cnpj { get; set; } = "41097595000122";
        [JsonProperty(PropertyName = "nome")] public string Name { get; set; } = "SampaioTur";

        // [JsonProperty(PropertyName = "oneOf")] public string OneOf { get; set; } = string.Empty;

    }

    public class Value
    {
        [JsonProperty(PropertyName = "original")]
        public string Original { get; set; }   
    }
}