using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace N1990.Episerver.Cms.Audit.Business
{
    [InitializableModule]
    public class DependencyResolverInitialisation : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.ConfigurationComplete += (o, e) => {
                context.Services.AddTransient<ICmsAuditor, CmsAuditor>();
            };
        }
    }
}