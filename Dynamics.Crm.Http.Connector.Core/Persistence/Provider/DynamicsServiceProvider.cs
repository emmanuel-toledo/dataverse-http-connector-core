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
	}
}
