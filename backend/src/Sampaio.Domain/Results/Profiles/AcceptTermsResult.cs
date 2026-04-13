namespace Sampaio.Domain.Results.Profiles
{
    public class AcceptTermsResult
    {
        public string Message { get; set; }
        public bool Success { get; set; } = false;
        
        public object User { get; set; }

        public string AccessToken { get; set; }
        
    }
}