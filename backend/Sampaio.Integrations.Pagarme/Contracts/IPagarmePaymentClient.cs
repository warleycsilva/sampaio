using System.Threading.Tasks;
using RestEase;
using Sampaio.Integrations.Pagarme.Models;

namespace Sampaio.Integrations.Pagarme.Contracts
{
    public interface IPagarmePaymentClient
    {
        [Header("Authorization")] public string ApiKey { get; set; }
        
        [Post("https://api.pagar.me/core/v5/orders")]
        Task<GetOrderResponse> CreateOrderAsync([Body] CreateOrderRequest request);
        
        [Get("https://api.pagar.me/core/v5/orders/{id}")]
        Task<GetOrderResponse> GetOrderAsync([Path(UrlEncode = false)] string id);
    }
}