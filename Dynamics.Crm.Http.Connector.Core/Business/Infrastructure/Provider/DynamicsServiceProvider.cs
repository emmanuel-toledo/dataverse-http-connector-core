using Microsoft.Extensions.DependencyInjection;

namespace Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Provider
{
    internal class DynamicsServiceProvider
    {
        public ServiceCollection Services { get; set; } = new ServiceCollection();
    }
}
