using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Dynamics.Crm.Http.Connector.Core.Microsoft.Extensions.DependencyInjections
{
    public static class ServiceExtensions
    {
        public static void UseApplicationUser(this IServiceCollection services, DynamicsConnection connection)
        {

        }

        public static void UseApplicationUser(this IServiceCollection services, IConfigurationSection configurationSection)
        {

        }
    }
}
