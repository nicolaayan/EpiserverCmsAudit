using System;
using System.Linq;
using EPiServer.Shell.Modules;
using N1990.Episerver.Cms.Audit.Business;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuditServices(this IServiceCollection services)
        {
            services.AddTransient < ICmsAuditor, CmsAuditor > ();

            services.Configure<ProtectedModuleOptions>(
                pm =>
                {
                    if (!pm.Items.Any(i => i.Name.Equals("CmsAudit", StringComparison.OrdinalIgnoreCase)))
                    {
                        pm.Items.Add(new ModuleDetails { Name = "CmsAudit" });
                    }
                });

                
            return services;
        }
    }
}
