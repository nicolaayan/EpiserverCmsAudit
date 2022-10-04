using N1990.Episerver.Cms.Audit.Business;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuditServices(this IServiceCollection services)
        {
            services.AddTransient <ICmsAuditor, CmsAuditor> ();

            return services;
        }
    }
}
