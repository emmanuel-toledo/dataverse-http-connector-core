using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;
using Microsoft.Identity.Client;

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
        /// Set a new Dynamics connection.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        void SetConnection(DynamicsConnection connection);

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
        bool IsValidAuthentication(string scope);
    }
}
