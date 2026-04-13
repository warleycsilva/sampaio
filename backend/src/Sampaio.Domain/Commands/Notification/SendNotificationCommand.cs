using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.Notification;

namespace Sampaio.Domain.Commands.Notification
{
    public class SendNotificationCommand : IRequest<SendNotificationResult>
    {
        [Required]
        public string Title { get;  set; }
        [Required]
        public string Message { get;  set; }
        
        public bool SendToDrivers { get;  set; }
        
        public bool SendToCommerces { get;  set; }
    }
}