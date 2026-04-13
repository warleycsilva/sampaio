namespace Sampaio.Domain.CommandsMessages
{
    public static class PaymentCommandMessages
    {
        public static readonly string CreateSuccessful = "Pagamento cadastrado com sucesso";
        public static readonly string UpdateSuccessful = "Pagamento atualizado com sucesso";
        public static readonly string DeleteSuccessful = "Pagamento deletado com sucesso";
        public static readonly string CreateFailed = "Falha ao cadastrar pagamento";
        public static readonly string UpdateFailed = "Falha ao atualizar pagamento";
        public static readonly string DeleteFailed = "Falha ao deletar pagamento";
        public static readonly string PaymentNotFound = "Falha ao encontrar pagamento";
    }
}