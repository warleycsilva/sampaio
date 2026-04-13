using Sampaio.Shared.Constants;

namespace Sampaio.Domain.CommandsMessages
{
    public class ProfileCommandMessages
    {
        public static readonly string UserInactiveFriendly = "E-mail inativo. Verifique sua caixa de entrada.";
        
        public static readonly string UserInactive = "Usuário Inativo";

        public static readonly string UserNotFound = "Usuário não encontrado.";
        
        public static readonly string ReSendEmailSuccess = "E-mail re-enviado com sucesso.";
        
        public static readonly string SendEmailSuccess = "E-mail enviado com sucesso";
        
        public static readonly string ProcessingError = "Erro durante o processo, tente mais tarde";
        
        public static readonly string PasswordChangeSuccess = "Senha alterada com sucesso";
        
        public static readonly string InvalidToken = "Token invalido";
        
        public static readonly string AceptTermsSuccess = "Termos foram aceitos com sucesso";
        
        public static readonly string AlreadyActivedUser = "Essa conta já foi ativada";

        public static readonly string ActiveAccountSuccess = "Conta foi ativada com sucesso";
        
        public static readonly string UpdateSuccess = "Sua conta foi atualizada com sucesso";
        
        public static readonly string UpdatePhotoSuccess = "Sua Foto foi atualizada com sucesso";

        public static readonly string FirstnameRequiredError = CommonMessages.BuiltRequiredMessageError("Nome");

        
        public static readonly string PasswordRequiredError = CommonMessages.BuiltRequiredMessageError("Senha");

        public static readonly string NewPasswordRequiredError = CommonMessages.BuiltRequiredMessageError("Confirmar Senha");

        public static readonly string NewPasswordConfirmationRequiredError = CommonMessages.BuiltRequiredMessageError("Confirmar Senha");

        
        public static readonly string NewPassMatchError = "As senhas devem conferir";
        

        
    }
}