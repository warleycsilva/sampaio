using System.Threading.Tasks;
using Sampaio.Domain.Models.Notifications;

namespace Sampaio.Domain.Contracts.Services
{
    public interface IPushNotificationService
    {
        Task SendMessage(SingleCastPushNotification pushNotification);
        Task SendMessage(MultiCastPushNotification pushNotification);
    }
}