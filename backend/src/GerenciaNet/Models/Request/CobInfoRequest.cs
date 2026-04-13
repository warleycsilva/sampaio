using Newtonsoft.Json;

namespace GerenciaNet.Models.Request
{
    public class CobInfoRequest
    {
        [JsonIgnore]
        public string CobId { get; set; }
    }
}