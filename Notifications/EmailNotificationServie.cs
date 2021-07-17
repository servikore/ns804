using Domain.Services;
using System.Threading.Tasks;

namespace Notifications
{
    public class EmailNotificationServie : INotificationService
    {
        public Task Notifify(NotificationOption notificationOption)
        {
            return Task.CompletedTask;
        }
    }
}
