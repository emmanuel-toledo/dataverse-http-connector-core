using Microsoft.Extensions.DependencyInjection;
using Dynamics.Crm.Http.Connector.Core.Persistence;
using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Facades.Generics.Queries;
using Dynamics.Crm.Http.Connector.Core.Business.Generic.Queries;
using Dynamics.Crm.Http.Connector.Core.Business.Generic.Commands;
using Dynamics.Crm.Http.Connector.Core.Context;
using Dynamics.Crm.Http.Connector.Core.Business.Commands;
using Dynamics.Crm.Http.Connector.Core.Business.Queries;
using Dynamics.Crm.Http.Connector.Core.Business.Handler;

namespace Dynamics.Crm.Http.Connector.Core.Extensions.Configurations
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
            // Configure Generic facade services.
            services.ConfigureFacadeGenericServices();
            // Configure Dynamics facade queries.
            services.ConfigureFacadeQueriesServices();
            // Configure Dynamics facade commands.
            services.ConfigureFacadeCommandsServices();
            // Configure Business services.
            services.ConfigureBusinessService();
            // Configure Dynamics context services.
            services.ConfigureDynamicsContext();
        }

        /// <summary>
        /// Function to configure all the main services to be used in each HTTP request to Dynamics CRM.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureBusinessService(this IServiceCollection services)
        {
            // Configure generic queries service.
            services.AddScoped<IDynamicsGenericQueries, DynamicsGenericQueries>();
            // Configure generic commands service.
            services.AddScoped<IDynamicsGenericCommands, DynamicsGenericCommands>();
            // Configure Dynamics queries service.
            services.AddScoped<IDynamicsQueries, DynamicsQueries>();
            // Configure Dynamics commands service.
            services.AddScoped<IDynamicsCommands, DynamicsCommands>();
            // Configure Dynamics request handler service.
            services.AddScoped<IRequestHandler, RequestHandler>();
        }

        /// <summary>
        /// Function to configure each service (commands and queries) that comes from a generic request.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureFacadeGenericServices(this IServiceCollection services)
        {
            services.AddScoped<IEntitiesDeffinitions, EntitiesDeffinitions>();
            services.AddScoped<IRetriveById, RetriveById>();
            services.AddScoped<IRetriveByFetch, RetriveByFetch>();
            services.AddScoped<IRetriveByOData, RetriveByOData>();
        }

        internal static void ConfigureFacadeQueriesServices(this IServiceCollection services)
        {

        }

        internal static void ConfigureFacadeCommandsServices(this IServiceCollection services)
        {

        }

        /// <summary>
        /// Function to configure main Dynamics request service and Dynamics context service.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureDynamicsContext(this IServiceCollection services)
        {
            // Configure Dynamics Request service.
            services.AddScoped<IDynamicsRequest, DynamicsRequest>();
            // Configure DbEntitySet service for Dynamics as an generic service.
            services.AddScoped(typeof(IDbEntitySet<>), typeof(DbEntitySet<>));
            // Configure main Dynamics service.
            services.AddScoped<IDynamicsContext, DynamicsContext>();
        }
    }
}
