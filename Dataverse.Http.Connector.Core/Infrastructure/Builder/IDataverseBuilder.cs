using Microsoft.Identity.Client;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;
using Dataverse.Http.Connector.Core.Infrastructure.Builder.Options;

namespace Dataverse.Http.Connector.Core.Infrastructure.Builder
{
    /// <summary>
    /// This interface defines configuration, methods and properties to connect and transform data from Dataverse.
    /// </summary>
    public interface IDataverseBuilder : IDataverseOptionsBuilder
    {
        /// <summary>
        /// Throw exception flag.
        /// </summary>
        bool ThrowExceptions { get; }

        /// <summary>
        /// Dataverse connection object.
        /// </summary>
        DataverseConnection? Connection { get; }

        /// <summary>
        /// Cached authentication result.
        /// </summary>
        AuthenticationResult? Authentication { get; }

        /// <summary>
        /// Dataverse connections collection.
        /// </summary>
        ICollection<DataverseConnection> Connections { get; }

        /// <summary>
        /// Set connection as principal.
        /// </summary>
        /// <param name="connection">Dataverse connection object.</param>
        void SetDefaultConnection(DataverseConnection connection);

        /// <summary>
        /// Function to add multiples Dataverse connections to the builder.
        /// <para>
        /// Set as default connection the first element of the collection.
        /// </para>
        /// </summary>
        /// <param name="connections">Dataverse connection collection.</param>
        void AddConnections(IEnumerable<DataverseConnection> connections);

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
        /// Function to validate if exists a previous authentication token for specific Dataverse environment.
        /// </summary>
        /// <param name="scope">Environment URL for Dataverse connection.</param>
        bool IsValidAuthentication(string scope);

        /// <summary>
        /// Function to change the connection as principal to connect in the runtime.
        /// </summary>
        /// <param name="connection">Set Dataverse environment connection as principal to connect.</param>
        void ChangeEnvironmentConnection(DataverseConnection? connection);
    }
}
