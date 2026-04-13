using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.CommandsMessages
{
    public static class PlanCommandMessages
    {
        public static readonly string CreateSuccessful = "Plano adicionado com sucesso";
        public static readonly string DebtCreated = "Sucesso! O plano será atualizado assim que o pagamento for recebido.";
        public static readonly string UpdateSuccessful = "Plano atualizado com sucesso";
        public static readonly string DeleteSuccessful = "Plano deletado com sucesso";
        public static readonly string CreateFailed = "Falha ao adicionar plano";
        public static readonly string UpdateFailed = "Falha ao atualizar plano";
        public static readonly string DeleteFailed = "Falha ao deletar plano";
        public static readonly string PlanNotFound = "Falha ao encontrar Plano";
    }
}
