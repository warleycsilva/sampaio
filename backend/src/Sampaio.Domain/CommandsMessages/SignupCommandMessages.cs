using Sampaio.Shared.Constants;

namespace Sampaio.Domain.CommandsMessages
{
    public static class SignupCommandMessages
    {
        public static readonly string EmailRequiredError = CommonMessages.BuiltRequiredMessageError("E-mail");
        public static readonly string EmailMaxLengthError = CommonMessages.BuiltMaxLenghMessageError("E-mail", 255);
        public static readonly string PasswordRequiredError = CommonMessages.BuiltRequiredMessageError("Senha");
        public static readonly string PasswordConfirmationRequiredError = CommonMessages.BuiltRequiredMessageError("Confirmação da senha");
        public static readonly string PasswordsNotMatchError = "Senhas não conferem";
        public static readonly string EmailInvalidError = "E-mail é inválido";
        public static readonly string FirstNameRequiredError = CommonMessages.BuiltRequiredMessageError("Nome");
        public static readonly string FirstNameMaxLengthError = CommonMessages.BuiltMaxLenghMessageError("Nome", 255);
        public static readonly string LastNameMaxLengthError = CommonMessages.BuiltMaxLenghMessageError("Sobrenome", 255);
        public static readonly string TypeInvalidError = "Tipo informado é inválido";
        public static readonly string SignUpSucess = "Cadastro efetuado com sucesso! Verifique seu email para ativar a conta";
        public static readonly string BackofficeSignUpSucess = "Cadastro efetuado com sucesso! Aguarde a aprovação por um administrador";
        
        public static readonly string EmptyToken = "Token Invalido";

        
    }
}
