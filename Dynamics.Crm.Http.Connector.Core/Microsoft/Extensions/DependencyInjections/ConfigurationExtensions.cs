using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Microsoft.Extensions.DependencyInjections
{
    internal static class ConfigurationExtensions
    {
        /// <summary>
        /// Function to initialize a "Transient" service for "IDynamicsBuilder" interface.
        /// </summary>
        /// <param name="services">Application service collection.</param>
        /// <param name="actionBuilder">Dynamics Builder Configuration.</param>
        internal static void ConfigureDynamicsBuilder(this IServiceCollection services, Action<DynamicsBuilder> actionBuilder)
            => services.AddTransient<IDynamicsBuilder, DynamicsBuilder>((serviceProvider) =>
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

        internal static void ConfigureClients(this IServiceCollection services)
        {
            
        }

        internal static void ConfigureFacadeQueriesServices(this IServiceCollection services)
        {

        }

        internal static void ConfigureFacadeCommandsServices(this IServiceCollection services)
        {

        }
    }
}
