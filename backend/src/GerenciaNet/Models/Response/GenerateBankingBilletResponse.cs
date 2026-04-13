using System;
using Newtonsoft.Json;

namespace GerenciaNet.Models.Response
{
    public class GenerateBankingBilletResponse
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("pdf")]
        public Pdf Pdf { get; set; }

        [JsonProperty("expire_at")]
        public DateTimeOffset ExpireAt { get; set; }

        [JsonProperty("charge_id")]
        public string ChargeId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("payment")]
        public string Payment { get; set; }
    }

    public partial class Pdf
    {
        [JsonProperty("charge")]
        public string Charge { get; set; }
    }
}