using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.CommandsMessages
{
    public static class SaleCommandMessages
    {
        public static readonly string CreateSuccessful = "Venda realizada com sucesso";
        public static readonly string UpdateSuccessful = "Venda atualizada com sucesso";
        public static readonly string DeleteSuccessful = "Venda deletada com sucesso";
        public static readonly string CreateFailed = "Falha ao realizar cadastro";
        public static readonly string UpdateFailed = "Falha ao atualizar venda";
        public static readonly string DeleteFailed = "Falha ao deletar venda";
        public static readonly string SaleNotFound = "Falha ao encontrar venda";
        public static readonly string Complete = "Venda finalizada com sucesso!\n Obrigado por usar a SampaioTur";
        public static readonly string Finalized = "Venda finalizada com sucesso! Obrigado por usar a SampaioTur";
        public static readonly string FailToFinalize = "Falha ao finalizar a venda";
    }
}
