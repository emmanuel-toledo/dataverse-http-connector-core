using Microsoft.Extensions.DependencyInjection;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Extensions.Configurations;

namespace Dynamics.Crm.Http.Connector.Core.Extensions.DependencyInjections
{
    public static class ServiceExtensions
    {
        public static void AddDynamicsContext<TContext>(this IServiceCollection services, Action<DynamicsBuilder> actionBuilder) where TContext : DynamicsContext
        {
            // Configure Dynamics Builder configuration (entites and connection information)
            services.ConfigureDynamicsBuilder(actionBuilder);
            // Configure Dynamics Authenticator service.
            services.ConfigureAuthenticator();
            // Configure HttpClient service.
            services.ConfigureServiceClient();
            // Configure Dynamics request service.
            services.ConfigureDynamicsService();
        }
    }
}
