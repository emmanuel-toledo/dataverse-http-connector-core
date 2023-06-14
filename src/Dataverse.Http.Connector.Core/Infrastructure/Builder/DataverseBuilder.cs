using Microsoft.Identity.Client;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;
using Dataverse.Http.Connector.Core.Infrastructure.Builder.Options;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse;

namespace Dataverse.Http.Connector.Core.Infrastructure.Builder
{
    /// <summary>
    /// This class implements configuration, methods and properties to connect and transform data from Dataverse.
    /// </summary>
    public class DataverseBuilder : DataverseOptionsBuilder, IDataverseBuilder
    {
        /// <summary>
        /// Get and set throw exception flag.
        /// </summary>
        private bool _throwExceptions { get; set; } = true;

        /// <summary>
        /// Get and set Dataverse connection object.
        /// </summary>
        private DataverseConnection? _connection { get; set; } = null;

        /// <summary>
        /// Get and set Authentication result.
        /// </summary>
        private AuthenticationResult? _authentication = null;

        /// <summary>
        /// Get and set Dataverse connections collection.
        /// </summary>
        private ICollection<DataverseConnection> _connections { get; set; } = new HashSet<DataverseConnection>();

        /// <summary>
        /// Dataverse Builder instance servive.
        /// </summary>
        public DataverseBuilder() { }

        /// <summary>
        /// Dataverse Builder instance servive.
        /// </summary>
        /// <param name="connection">Dataverse connection object.</param>
        public DataverseBuilder(DataverseConnection connection)
            => SetDefaultConnection(connection);

        /// <summary>
        /// Dataverse Builder instance servive.
        /// </summary>
        /// <param name="connection">Dataverse connection object.</param>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        public DataverseBuilder(DataverseConnection connection, bool throwExceptions)
        {
            SetDefaultConnection(connection);
            SetThrowExceptions(throwExceptions);
        }

        /// <summary>
        /// Dataverse Builder instance servive.
        /// </summary>
        /// <param name="connections">Dataverse connections collection.</param>
        /// <param name="connection">Dataverse connection object.</param>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        public DataverseBuilder(ICollection<DataverseConnection> connections, DataverseConnection connection, bool throwExceptions)
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
        /// Get Dataverse connection object.
        /// </summary>
        public DataverseConnection? Connection { get => _connection; }

        /// <summary>
        /// Cached authentication result.
        /// </summary>
        public AuthenticationResult? Authentication { get => _authentication; }

        /// <summary>
        /// Dataverse connections collection.
        /// </summary>
        public ICollection<DataverseConnection> Connections { get => _connections ?? new HashSet<DataverseConnection>(); }

        /// <summary>
        /// Set connection as principal.
        /// </summary>
        /// <param name="connection">Dataverse connection object.</param>
        public void SetDefaultConnection(DataverseConnection connection)
        {
            if (!_connections.Contains(connection))
                _connections.Add(connection);
            _connection = connection;
        }

        /// <summary>
        /// Function to add multiples Dataverse connections to the builder.
        /// <para>
        /// Set as default connection the first element of the collection.
        /// </para>
        /// </summary>
        /// <param name="connections">Dataverse connection collection.</param>
        public void AddConnections(IEnumerable<DataverseConnection> connections)
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
        /// Function to validate if exists a previous authentication token for specific Dataverse environment.
        /// </summary>
        public bool IsValidAuthentication(string scope)
            => _authentication is not null &&
               _authentication.Scopes.Any(x => x.Contains(scope)) &&
               DateTime.Now < _authentication.ExpiresOn;

        /// <summary>
        /// Function to change the connection as principal to connect in the runtime.
        /// </summary>
        /// <param name="connection">Set Dataverse environment connection as principal to connect.</param>
        /// <exception cref="NotFoundException">Connection could not be found.</exception>
        public void ChangeEnvironmentConnection(DataverseConnection? connection)
        {
            if(connection is null)
                throw new NotFoundException("The connection does not exists and is not configured.");
            _connection = connection;
        }
    }
}
