namespace Sampaio.Domain.Results.Commerce
{
    public class CommerceSignupResult
    {
        public string Error{ get; set; }
        
        public bool Success { get; set; } = false;
        
        public object User { get; set; }

        public string AccessToken { get; set; }
        
        public string Message { get; set; }
    }
}