namespace Sampaio.Shared.Constants
{
    public class CommonMessages
    {
        public static readonly string NotFound = "Not Found";

        public static readonly string UnprocessableEntity = "Unprocessable Entity";

        public static readonly string Unauthorized = "Unauthorized";

        public static readonly string Forbidden = "Forbidden";

        public static readonly string InternalServerError = "Internal Server Error";

        public static readonly string ProblemSavindData = "Ocorreu um problema ao salvar os dados";

        public static readonly string ProblemSavindDataFriendly = "Ops, não foi possível processar sua requisição no momento. Por favor tente novamente mais tarde";

        public static readonly string InvalidCredentials = "E-mail e/ou senha inválido(s)";

        public static readonly string InvalidProfile = "Ação não permitida para o perfil do usuário informado.";

        public static string BuiltRequiredMessageError(string propertyName) => $"{propertyName} é requerido";

        public static string BuiltMaxLenghMessageError(string propertyName,
            int maxLength) => $"{propertyName} deve conter no máximo {maxLength} caracteres";
    }
}
