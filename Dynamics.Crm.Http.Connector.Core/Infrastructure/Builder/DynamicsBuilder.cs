using Microsoft.Identity.Client;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder
{
    /// <summary>
    /// This class implements configuration, methods and properties to connect and transform data from Dynamics.
    /// </summary>
    public class DynamicsBuilder : DynamicsOptionsBuilder, IDynamicsBuilder, IDynamicsOptionsBuilder
    {
        /// <summary>
        /// Get and set Dynamics connection object.
        /// </summary>
        private DynamicsConnection? _connection { get; set; } = null;

        /// <summary>
        /// Get and set throw exception flag.
        /// </summary>
        private bool _throwExceptions { get; set; } = true;

        /// <summary>
        /// Get and set Authentication result.
        /// </summary>
        private AuthenticationResult? _authentication = null;

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        public DynamicsBuilder() { }

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        public DynamicsBuilder(DynamicsConnection connection)
            => _connection = connection;

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        public DynamicsBuilder(DynamicsConnection connection, bool throwExceptions)
        {
            _connection = connection;
            _throwExceptions = throwExceptions;
        }

        /// <summary>
        /// Get throw exception flag.
        /// </summary>
        public bool ThrowExceptions { get => _throwExceptions; }

        /// <summary>
        /// Get Dynamics connection object.
        /// </summary>
        public DynamicsConnection? Connection { get => _connection; }

        /// <summary>
        /// Cached authentication result.
        /// </summary>
        public AuthenticationResult? Authentication { get => _authentication; }

        /// <summary>
        /// Set a new Dynamics connection.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        public void SetConnection(DynamicsConnection connection)
            => _connection = connection;

        /// <summary>
        /// Set throw exceptions flag.
        /// </summary>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        public void SetThrowExceptions(bool throwExceptions)
            => _throwExceptions = throwExceptions;

        /// <summary>
        /// Set authentication result.
        /// </summary>
        /// <param name="authentication">Throw exceptions flag.</param>
        public void SetAuthenticationResult(AuthenticationResult? authentication)
            => _authentication = authentication;

        /// <summary>
        /// Function to validate if exists a previous authentication token for specific Dynamics environment.
        /// </summary>
        public bool IsValidAuthentication(string scope)
            => _authentication is not null && 
               _authentication.Scopes.Any(x => x.Contains(scope)) && 
               DateTime.Now < _authentication.ExpiresOn;
    }
}
