using System.Threading.Tasks;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class BaseCommandHandler
    {
        private readonly IUnitOfWork _uow;

        protected IDomainNotification Notifications;
        protected BaseCommandHandler(IUnitOfWork uow, IDomainNotification notifications )
        {
            _uow = uow;
            Notifications = notifications;
        }

        private protected async Task<bool> CommitAsync()
        {
            if (Notifications.HasNotifications()) return false;

            if (await _uow.SaveChangesAsync() > 0) return true;

            Notifications.Handle(CommonMessages.ProblemSavindData);

            return false;
        }
    }
}
