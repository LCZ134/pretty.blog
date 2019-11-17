using Autofac;
using Pretty.WebFramework.Factories;

namespace Pretty.Web.Inject
{
    public class FactoryInject: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(IBlogModelFactory).Assembly)
                 .Where(t => t.Name.EndsWith("Factory"))
                 .AsImplementedInterfaces();
        }
    }
}
