using Autofac;
using Domain.Services;


namespace WebAPI.App_Start
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly ILifetimeScope container;

        public NotificationFactory(ILifetimeScope container)
        {
            this.container = container;
        }
        public INotificationService GetNotificationService(NotificationTypes notificationTypes)
        {
            return container.ResolveKeyed<INotificationService>(notificationTypes);
        }
    }
}