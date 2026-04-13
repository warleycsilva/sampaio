using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.CommandsMessages
{
    public static class SaleItemCommandMessages
    {
        public static readonly string CreateSuccessful = "Cadastro realizado com sucesso";
        public static readonly string UpdateSuccessful = "Cadastro atualizado com sucesso";
        public static readonly string DeleteSuccessful = "Cadastro deletado com sucesso";
        public static readonly string CreateFailed = "Falha ao realizar cadastro";
        public static readonly string UpdateFailed = "Falha ao atualizar cadastro";
        public static readonly string DeleteFailed = "Falha ao deletar cadastro";
        public static readonly string SaleItemNotFound = "Falha ao encontrar SaleItem";
    }
}
