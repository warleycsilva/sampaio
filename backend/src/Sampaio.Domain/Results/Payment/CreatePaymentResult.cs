using System;

namespace Sampaio.Domain.Results.Payment
{
    public class CreatePaymentResult
    {
        public Guid PaymentId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}