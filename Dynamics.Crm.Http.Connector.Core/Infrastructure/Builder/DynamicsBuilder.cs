using Microsoft.Identity.Client;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Connection;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder
{
    /// <summary>
    /// This class implements configuration, methods and properties to connect and transform data from Dynamics.
    /// </summary>
    public class DynamicsBuilder : DynamicsOptionsBuilder, IDynamicsBuilder
    {
        /// <summary>
        /// Get and set throw exception flag.
        /// </summary>
        private bool _throwExceptions { get; set; } = true;

        /// <summary>
        /// Get and set Dynamics connection object.
        /// </summary>
        private DynamicsConnection? _connection { get; set; } = null;

        /// <summary>
        /// Get and set Authentication result.
        /// </summary>
        private AuthenticationResult? _authentication = null;

        /// <summary>
        /// Get and set Dynamics connections collection.
        /// </summary>
        private ICollection<DynamicsConnection> _connections { get; set; } = new HashSet<DynamicsConnection>();

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        public DynamicsBuilder() { }

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        public DynamicsBuilder(DynamicsConnection connection)
            => SetDefaultConnection(connection);

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        public DynamicsBuilder(DynamicsConnection connection, bool throwExceptions)
        {
            SetDefaultConnection(connection);
            SetThrowExceptions(throwExceptions);
        }

        /// <summary>
        /// Dynamics Builder instance servive.
        /// </summary>
        /// <param name="connections">Dynamics connections collection.</param>
        /// <param name="connection">Dynamics connection object.</param>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        public DynamicsBuilder(ICollection<DynamicsConnection> connections, DynamicsConnection connection, bool throwExceptions)
        {
            AddConnections(connections);
            SetDefaultConnection(connection);
            SetThrowExceptions(throwExceptions);
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
        /// Dynamics connections collection.
        /// </summary>
        public ICollection<DynamicsConnection> Connections { get => _connections ?? new HashSet<DynamicsConnection>(); }

        /// <summary>
        /// Set connection as principal.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        public void SetDefaultConnection(DynamicsConnection connection)
        {
            if (!_connections.Contains(connection))
                _connections.Add(connection);
            _connection = connection;
        }

        /// <summary>
        /// Function to add multiples Dynamics connections to the builder.
        /// <para>
        /// Set as default connection the first element of the collection.
        /// </para>
        /// </summary>
        /// <param name="connections">Dynamics connection collection.</param>
        public void AddConnections(IEnumerable<DynamicsConnection> connections)
        {
            var distinct = connections.Distinct().ToList();
            _connections = distinct;
            _connection = connections.FirstOrDefault();
        }

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

        /// <summary>
        /// Function to change the connection as principal to connect in the runtime.
        /// </summary>
        /// <param name="connection">Set Dynamics environment connection as principal to connect.</param>
        public void ChangeEnvironmentConnection(DynamicsConnection? connection)
        {
            if(connection is null)
                throw new NotFoundException("The connection does not exists and is not configured.");
            _connection = connection;
        }
    }
}
