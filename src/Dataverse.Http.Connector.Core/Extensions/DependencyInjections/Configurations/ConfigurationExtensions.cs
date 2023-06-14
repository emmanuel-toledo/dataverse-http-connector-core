using Microsoft.Extensions.DependencyInjection;
using Dataverse.Http.Connector.Core.Context;
using Dataverse.Http.Connector.Core.Persistence;
using Dataverse.Http.Connector.Core.Business.Queries;
using Dataverse.Http.Connector.Core.Business.Handler;
using Dataverse.Http.Connector.Core.Facades.Requests;
using Dataverse.Http.Connector.Core.Business.Commands;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Business.Authentication;

namespace Dataverse.Http.Connector.Core.Extensions.DependencyInjections.Configurations
{
    internal static class ConfigurationExtensions
    {
        /// <summary>
        /// Function to initialize a "Transient" service for "IDataverseBuilder" interface.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="actionBuilder">Dataverse Builder Configuration.</param>
        internal static void ConfigureDataverseBuilder(this IServiceCollection services, Action<DataverseBuilder> actionBuilder)
            => services.AddScoped<IDataverseBuilder, DataverseBuilder>((serviceProvider) =>
            {
                DataverseBuilder builder = new();
                actionBuilder(builder);
                return builder;
            });

        /// <summary>
        /// Function to initialize a "Scoped" service for "IDataverseAuthenticator" interface.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureAuthenticator(this IServiceCollection services)
            => services.AddScoped<IDataverseAuthenticator, DataverseAuthenticator>();

        /// <summary>
        /// Function to configure HTTP Client to with configuration from different services to use in each request with Dataverse .
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureServiceClient(this IServiceCollection services)
            => services.AddHttpClient("DataverseClient", client => client.ConfigureDefaultClientConfiguration());

        /// <summary>
        /// Function to configure Dataverse Service to be used in each HTTP request to Dataverse .
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureDataverseServices(this IServiceCollection services)
        {
            // Configure Business services.
            services.ConfigureBusinessService();
            // Configure Dataverse context services.
            services.ConfigureDataverseContext();
        }

        /// <summary>
        /// Function to configure all the main services to be used in each HTTP request to Dataverse .
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureBusinessService(this IServiceCollection services)
        {
            // Configure Dataverse queries service.
            services.AddScoped<IDataverseQueries, DataverseQueries>();
            // Configure Dataverse commands service.
            services.AddScoped<IDataverseCommands, DataverseCommands>();
            // Configure Dataverse request handler service.
            services.AddScoped<IRequestHandler, RequestHandler>();
            // Configure Parse response handler service.
            services.AddScoped<IParseHandler, ParseHandler>();
        }

        /// <summary>
        /// Function to configure main Dataverse request service and Dataverse context service.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        internal static void ConfigureDataverseContext(this IServiceCollection services)
        {
            // Configure Dataverse Request service.
            services.AddScoped<IDataverseRequest, DataverseRequest>();
            // Configure DbEntitySet service for Dataverse as an generic service.
            services.AddTransient(typeof(IDbEntitySet<>), typeof(DbEntitySet<>));
            // Configure main Dataverse service.
            services.AddScoped<IDataverseContext, DataverseContext>();
        }
    }
}
