namespace Sampaio.Domain.Results.Payment
{
    public class DeletePaymentResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}