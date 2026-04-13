namespace Sampaio.Integrations.Pagarme
{
    
    public sealed class PagarmeIntegrationOptions
    {
        public string BaseUrl { get; set; }= default!;
        public string ContaId { get; set; }= default!;
        public string PublicKey { get; set; }= default!;
        public string SecretKey { get; set; }= default!;
    }
}