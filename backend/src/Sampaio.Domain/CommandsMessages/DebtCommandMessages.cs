using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.CommandsMessages
{
    public static class DebtCommandMessages
    {
        public static readonly string CreateSuccessful = "Cadastro de débito criado com sucesso";
        public static readonly string UpdateSuccessful = "Cadastro de débito atualizado com sucesso";
        public static readonly string DeleteSuccessful = "Cadastro de débito deletado com sucesso";
        public static readonly string CreateFailed = "Falha ao realizar cadastro de débito";
        public static readonly string UpdateFailed = "Falha ao atualizar cadastro de débito";
        public static readonly string DeleteFailed = "Falha ao deletar cadastro de débito";
        public static readonly string DebtNotFound = "Nenhum débito em aberto.";
    }
}
