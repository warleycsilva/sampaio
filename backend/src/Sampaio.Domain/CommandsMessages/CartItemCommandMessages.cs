namespace Sampaio.Domain.CommandsMessages
{
    public class CartItemCommandMessages
    {
        public static readonly string CreateSuccessful = "Item adicionado ao carrinho com sucesso";
        public static readonly string UpdateSuccessful = "Item atualizado no carrinho com sucesso";
        public static readonly string DeleteSuccessful = "Item deletado do carrinho com sucesso";
        public static readonly string CreateFailed = "Falha ao adicionar item no carrinho";
        public static readonly string ProductNotFound = "Erro: Produto não encontrado.";
        public static readonly string UpdateFailed = "Falha ao atualizar item no carrinho";
        public static readonly string DeleteFailed = "Falha ao deletar item do carrinho";
        public static readonly string CartItemNotFound = "Falha ao encontrar item no carrinho";
        public static readonly string CartItemFromDifferenteCommerce = "Deve-se adicionar produtos da mesma loja para continuar.";
    }
}