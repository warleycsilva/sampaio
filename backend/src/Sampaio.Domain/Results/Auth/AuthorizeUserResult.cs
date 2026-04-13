namespace Sampaio.Domain.Results.Auth
{
    public class AuthorizeUserResult
    {
        public object User { get; set; }

        public string AccessToken { get; set; }

        public bool Success { get; set; } = false;

        public string Error { get; set; }
    }
}