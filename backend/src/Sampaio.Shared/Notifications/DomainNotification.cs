using System.Collections.Generic;
using System.Linq;
using Sampaio.Shared.Constants;

namespace Sampaio.Shared.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        public List<Notification> Values { get; private set; } = new List<Notification>();

        public void Handle(string value)
        {
            Values.Add(new Notification(value));
        }
        
        public void Handle(string key, string value)
        {
            Values.Add(new Notification(key, value));
        }
        
        public void Handle(Notification item)
        {
            Values.Add(item);
        }
        
        public IEnumerable<Notification> Notify()
        {
            if(!Values.Any())
                Handle(CommonMessages.ProblemSavindDataFriendly);
            return Values;
        }

        public bool HasNotifications()
        {
            return Values.Count > 0;
        }

        public void Dispose() => Values = new List<Notification>();
    }
}
