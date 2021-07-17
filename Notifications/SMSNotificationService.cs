
using Domain.Services;
using System.Threading.Tasks;

namespace Notifications
{
    public class SMSNotificationService : INotificationService
    {
        public Task Notifify(NotificationOption notificationOption)
        {
            return Task.CompletedTask;
        }
    }
}
