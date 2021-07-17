using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Services
{
    
    public interface INotificationService
    {
        Task Notifify(NotificationOption notificationOption);
    }
}
