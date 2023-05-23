using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Extensions.InternalInjection;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
