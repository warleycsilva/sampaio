namespace Sampaio.Domain.Results.Driver
{
    public class SelectDriverPlanResult
    {
        public bool Success { get; set; } = false;
        
        public string Message { get; set; }
        public string QrCode { get; set; }
        public string CopyPaste { get; set; }
        
    }
}