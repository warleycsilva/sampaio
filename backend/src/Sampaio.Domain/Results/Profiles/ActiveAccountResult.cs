namespace Sampaio.Domain.Results.Profiles
{
    public class ActiveAccountResult
    {
        public bool Success { get; set; } = false;
        
        public string Message { get; set; }
        public string Error { get; set; }
    }
}