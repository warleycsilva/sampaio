namespace Sampaio.Domain.CommandsMessages
{
    public class ProductCategoryCommandMessages
    {
        public static readonly string CreateSuccessful = "Categoria do produto cadastrada com sucesso";
        public static readonly string UpdateSuccessful = "Categoria do produto atualizada com sucesso";
        public static readonly string DeleteSuccessful = "Categoria do produto deletada com sucesso";
        public static readonly string CreateFailed = "Falha ao cadastrar categoria do produto";
        public static readonly string UpdateFailed = "Falha ao atualizar categoria do produto";
        public static readonly string DeleteFailed = "Falha ao deletar categoria do produto";
        public static readonly string ProductCategoryNotFound = "Falha ao encontrar categoria do produto";
    }
}