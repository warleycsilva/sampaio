namespace Sampaio.Domain.CommandsMessages
{
    public static class DebtPaymentCommandMessages
    {
        public static readonly string CreateSuccessful = "Pagamento de débito adicionado com sucesso";
        public static readonly string UpdateSuccessful = "Pagamento de débito atualizado com sucesso";
        public static readonly string DeleteSuccessful = "Pagamento de débito deletado com sucesso";
        public static readonly string CreateFailed = "Falha ao criar pagamento de débito";
        public static readonly string UpdateFailed = "Falha ao atualizar pagamento de débito";
        public static readonly string DeleteFailed = "Falha ao deletar pagamento de débito";
        public static readonly string DebtPaymentNotFound = "Falha ao encontrar pagamento de débito";
    }
}