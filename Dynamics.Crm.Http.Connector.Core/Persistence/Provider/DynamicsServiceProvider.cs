using Dynamics.Crm.Http.Connector.Core.Business.Handler;
using Dynamics.Crm.Http.Connector.Core.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamics.Crm.Http.Connector.Core.Persistence.Provider
{
	/// <summary>
	/// This class works as library internal service provider to get required services for some cases.
	/// </summary>
	internal static class DynamicsServiceProvider
	{
		/// <summary>
		/// Library internal service collection.
		/// </summary>
		public static IServiceCollection Services { get; private set; } = new ServiceCollection();

		/// <summary>
		/// Library internal service provider.
		/// </summary>
		public static IServiceProvider? Provider { get; private set; } = null;

		/// <summary>
		/// Function to build internal service provider.
		/// </summary>
		/// <returns>Service provider instance.</returns>
		internal static IServiceProvider BuildServices()
		{
			Provider = Services.BuildServiceProvider();
			return Provider;
		}

		/// <summary>
		/// Function to return IParseHandler service from Local Service Provider.
		/// </summary>
		/// <returns>IParseHandler service.</returns>
		internal static IParseHandler GetParseHandler()
		{
			if(Provider == null)
				BuildServices();
			return Provider!.GetRequiredService<IParseHandler>();
		}

        /// <summary>
        /// Function to return IRequestHandler service from Local Service Provider.
        /// </summary>
        /// <returns>IRequestHandler service.</returns>
        internal static IRequestHandler GetRequestHandler()
        {
            if (Provider == null)
                BuildServices();
            return Provider!.GetRequiredService<IRequestHandler>();
        }
    }
}
