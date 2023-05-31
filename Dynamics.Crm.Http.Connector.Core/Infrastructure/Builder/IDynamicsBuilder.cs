using Microsoft.Identity.Client;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Connection;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder
{
    /// <summary>
    /// This interface defines configuration, methods and properties to connect and transform data from Dynamics.
    /// </summary>
    public interface IDynamicsBuilder : IDynamicsOptionsBuilder
    {
        /// <summary>
        /// Throw exception flag.
        /// </summary>
        bool ThrowExceptions { get; }

        /// <summary>
        /// Dynamics connection object.
        /// </summary>
        DynamicsConnection? Connection { get; }

        /// <summary>
        /// Cached authentication result.
        /// </summary>
        AuthenticationResult? Authentication { get; }

        /// <summary>
        /// Dynamics connections collection.
        /// </summary>
        ICollection<DynamicsConnection> Connections { get; }

        /// <summary>
        /// Set connection as principal.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        void SetDefaultConnection(DynamicsConnection connection);

        /// <summary>
        /// Function to add multiples Dynamics connections to the builder.
        /// <para>
        /// Set as default connection the first element of the collection.
        /// </para>
        /// </summary>
        /// <param name="connections">Dynamics connection collection.</param>
        void AddConnections(IEnumerable<DynamicsConnection> connections);

        /// <summary>
        /// Set throw exceptions flag.
        /// </summary>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        void SetThrowExceptions(bool throwExceptions);

        /// <summary>
        /// Set authentication result.
        /// </summary>
        /// <param name="authentication">Throw exceptions flag.</param>
        void SetAuthenticationResult(AuthenticationResult? authentication);

        /// <summary>
        /// Function to validate if exists a previous authentication token for specific Dynamics environment.
        /// </summary>
        /// <param name="scope">Environment URL for Dynamics connection.</param>
        bool IsValidAuthentication(string scope);

        /// <summary>
        /// Function to change the connection as principal to connect in the runtime.
        /// </summary>
        /// <param name="connection">Set Dynamics environment connection as principal to connect.</param>
        void ChangeEnvironmentConnection(DynamicsConnection? connection);
    }
}
