using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sampaio.Integrations.Pagarme.Contracts;

namespace Sampaio.Integrations.Pagarme
{
    public class PagarmeClient
    {
        private readonly IPagarmePaymentClient _paymentClient;

        public IPagarmePaymentClient PaymentClient => _paymentClient;
        private string _secretKet = string.Empty;
        public PagarmeClient(IPagarmePaymentClient paymentClient,
            IOptions<PagarmeIntegrationOptions> options)
        {
            _paymentClient = paymentClient;
            _secretKet = options.Value.SecretKey;
        }

        public async ValueTask SetPaymentClientToken()
        {
            _paymentClient.ApiKey = string.Format("Basic {0}",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format(
                            "{0}:{1}",
                            _secretKet,
                            string.Empty))));
        }
    }
}