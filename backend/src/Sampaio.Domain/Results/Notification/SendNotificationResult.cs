namespace Sampaio.Domain.Results.Notification
{
    public class SendNotificationResult
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; } = false;
    }
}