namespace Sampaio.Domain.Results.BackofficeUser
{
    public class CompleteBackofficeSignUpResult
    {
        public string error{ get; set; }
        
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        
        public object User { get; set; }

        public string AccessToken { get; set; }
    }
}