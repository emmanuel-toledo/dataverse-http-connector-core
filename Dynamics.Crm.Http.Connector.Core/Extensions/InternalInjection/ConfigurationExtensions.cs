using Microsoft.Extensions.DependencyInjection;
using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Business.Authentication;

namespace Dynamics.Crm.Http.Connector.Core.Extensions.InternalInjection
{
    internal static class ConfigurationExtensions
    {
        /// <summary>
        /// Function to initialize a "Transient" service for "IDynamicsBuilder" interface.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="actionBuilder">Dynamics Builder Configuration.</param>
        internal static void ConfigureDynamicsBuilder(this IServiceCollection services, Action<DynamicsBuilder> actionBuilder)
            => services.AddScoped<IDynamicsBuilder, DynamicsBuilder>((serviceProvider) =>
            {
                DynamicsBuilder builder = new();
                actionBuilder(builder);
                return builder;
            });

        /// <summary>
        /// Function to initialize a "Scoped" service for "IDynamicsAuthenticator" interface.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureAuthenticator(this IServiceCollection services)
            => services.AddScoped<IDynamicsAuthenticator, DynamicsAuthenticator>();

        /// <summary>
        /// Function to configure HTTP Client to with configuration from different services to use in each request with Dynamics CRM.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureServiceClient(this IServiceCollection services)
            => services.AddHttpClient("DynamicsClient", client => client.ConfigureDefaultClientConfiguration());

        /// <summary>
        /// Function to configure Dynamics Service to be used in each HTTP request to Dynamics CRM.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureDynamicsService(this IServiceCollection services)
        {
            // Configure Dynamics Request Service.
            services.AddScoped<IDynamicsRequest, DynamicsRequest>();
            // Configure Dynamics facade queries.
            services.ConfigureFacadeQueriesServices();
            // Configure Dynamics facade commands.
            services.ConfigureFacadeCommandsServices();
            // Configure Dynamics context services.
            services.ConfigureDynamicsContext();
        }

        internal static void ConfigureFacadeQueriesServices(this IServiceCollection services)
        {

        }

        internal static void ConfigureFacadeCommandsServices(this IServiceCollection services)
        {

        }

        internal static void ConfigureDynamicsContext(this IServiceCollection services)
        {

        }
    }
}
