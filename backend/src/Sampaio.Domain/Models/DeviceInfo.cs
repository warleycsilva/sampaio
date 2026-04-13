using Sampaio.Shared.Extensions;
using Sampaio.Shared.Utils;
namespace Sampaio.Domain.Models
{
    public class DeviceInfo
    {
        public string DeviceId { get; set; }

        public string DeviceToken { get; set; }

        public bool IsValid() => DeviceId.NotNull() && DeviceToken.NotNull();
    }
}
