using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public enum NotificationTypes
    {
        Email = 1,
        SMS = 2
    }
    public interface INotificationFactory
    {
        INotificationService GetNotificationService(NotificationTypes notificationTypes);
    }    
}
