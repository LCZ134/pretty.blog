using Autofac;
using Octokit;
using Pretty.Core.Data;
using Pretty.Core.Domain.Securities;
using Pretty.Data;
using Pretty.Services.Customers;
using Pretty.Services.Events;
using Pretty.Services.Githubs;
using Pretty.Services.Settings;
using Pretty.Services.Users;

namespace Pretty.Web.Inject
{
    public class ServiceInject : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(SecuritySettings).Assembly)
                .InstancePerDependency();


            builder.RegisterAssemblyTypes(typeof(IUserService).Assembly)
                     .Where(t => t.Name.EndsWith("Service"))
                     .AsImplementedInterfaces()
                     .Except<IEventService>()
                     .Except<ISettingService>()
                     .Except<IGitHubService>()
                     .WithParameter(
                        new TypedParameter(typeof(IGitHubClient),
                        new GitHubClient(new ProductHeaderValue("pretty_app"))));

            builder.RegisterType<EventService>()
                .As<IEventService>()
                .SingleInstance();

        }
    }
}
