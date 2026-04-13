using Newtonsoft.Json;

namespace GerenciaNet.Models.Request
{
    public class PixRequest
    {
        [JsonIgnore] public int LocationId { get; set; }
    }
}