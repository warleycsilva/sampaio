using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.CommandsMessages
{
    public static class ProductCommandMessages
    {
        public static readonly string CreateSuccessful = "Produto cadastrado com sucesso";
        public static readonly string UpdateSuccessful = "Produto atualizado com sucesso";
        public static readonly string DeleteSuccessful = "Produto deletado com sucesso";
        public static readonly string SelectCategorySuccessful = "Categoria do produto selecionada com sucesso";
        public static readonly string CreateFailed = "Falha ao cadastrar produto";
        public static readonly string UpdateFailed = "Falha ao atualizar produto";
        public static readonly string DeleteFailed = "Falha ao deletar produto";
        public static readonly string ProductNotFound = "Nenhum produto cadastrado para o lojista.";
        public static readonly string SelectCategoryFailed = "Falha ao selecionar categoria do produto";
        public static readonly string PhotoProcessingError = "Erro durante o  da imagem, tente mais tarde";
        public static readonly string UpdateProductPhotoSuccess = "A Foto do produto foi atualizada com sucesso";
        public static readonly string DiscountPriceRequired = "O Valor com desconto deve ser preenchido!";

    }
}
