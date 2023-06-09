using Microsoft.Extensions.DependencyInjection;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Extensions.DependencyInjections.Configurations;

namespace Dataverse.Http.Connector.Core.Extensions.DependencyInjections
{
    /// <summary>
    /// Main class to implement Dataverse services.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure Dataverse context services.
        /// </summary>
        /// <typeparam name="TContext">TContext class of type DataverseContext.</typeparam>
        /// <param name="services">Application service collection.</param>
        /// <param name="actionBuilder">Dataverse Builder Configuration.</param>
        public static void AddDataverseContext<TContext>(this IServiceCollection services, Action<DataverseBuilder> actionBuilder) where TContext : DataverseContext
        {
            // Configure Dataverse Builder configuration (entites and connection information)
            services.ConfigureDataverseBuilder(actionBuilder);
            // Configure Dataverse Authenticator service.
            services.ConfigureAuthenticator();
            // Configure HttpClient service.
            services.ConfigureServiceClient();
            // Configure Dataverse request service.
            services.ConfigureDataverseServices();
        }
    }
}
