namespace Sampaio.Domain.CommandsMessages
{
    class PaymentCommandMessage
    {
        public static readonly string AuthorizedPayment = "Pagamento autorizado.";
        public static readonly string PaymentFailed = "Falha na autorização do pagamento.";
        public static readonly string PaymentNotFound = "Falha na encontrar pagamento.";
        
    }
}
