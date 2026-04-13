using Newtonsoft.Json;

namespace GerenciaNet.Models.Response
{
    public class PixResponse
    {
        [JsonProperty("qrcode")]
        public string Qrcode { get; set; }

        [JsonProperty("imagemQrcode")]
        public string ImagemQrcode { get; set; }
        
        public string CobId { get; set; }
        
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Response { get; set; }
    }
}