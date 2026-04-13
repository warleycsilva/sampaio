namespace Sampaio.Domain.CommandsMessages
{
    public class CartCommandMessages
    {
        public static readonly string CreateSuccessful = "Carrinho criado com sucesso";
        public static readonly string UpdateSuccessful = "Carrinho atualizado com sucesso";
        public static readonly string AbandonedSuccessful = "Carrinho abandonado com sucesso";
        public static readonly string CreateFailed = "Falha ao criar carrinho";
        public static readonly string UpdateFailed = "Falha ao atualizar carrinho";
        public static readonly string DeleteFailed = "Falha ao deletar carrinho";
        public static readonly string CartNotFound = "Falha ao encontrar carrinho";
        public static readonly string FinalizeCart = "Venda finalizada com sucesso";
        public static readonly string CartCommerceNotFound = "Falha ao declarar comércio pertencente.";
    }
}