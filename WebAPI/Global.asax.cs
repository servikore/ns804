using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Domain.Repositories;
using Domain.Services;
using EFInMemory;
using Microsoft.EntityFrameworkCore;
using Notifications;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.App_Start;
using WebAPI.Models;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //Handler for Jwt
            GlobalConfiguration.Configuration.MessageHandlers.Add(new JwtValidationHandler());

            //DI Configuration
            ContainerBuilder builder = new ContainerBuilder();
            
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).InstancePerRequest().PropertiesAutowired();

            builder.Register(x =>
            {
                var options = new DbContextOptionsBuilder<MemoryDbContext>()
                    .UseInMemoryDatabase(databaseName: "ns804").Options;

                return new MemoryDbContext(options);
            }).SingleInstance();

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            builder.RegisterType<EmailNotificationServie>().Keyed<INotificationService>(NotificationTypes.Email).InstancePerRequest();
            builder.RegisterType<SMSNotificationService>().Keyed<INotificationService>(NotificationTypes.SMS).InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();

            builder.RegisterType<MappingProfiles>().As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            //builder.RegisterType<LogWebApiActionAttribute>().PropertiesAutowired();
            builder.RegisterType<NotificationFactory>().As<INotificationFactory>();        

            IContainer container = builder.Build();

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //IExtensibilityManager extensibilityManager = container.Resolve<IExtensibilityManager>();

            //extensibilityManager.GetModuleEvents(); // stores them in state

            //GlobalFilters.Filters.Add(container.Resolve<LogMvcActionAttribute>());
            //GlobalConfiguration.Configuration.Filters.Add(container.Resolve<LogWebApiActionAttribute>());
        }
    }
}
