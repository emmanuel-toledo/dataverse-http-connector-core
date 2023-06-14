using Dataverse.Http.Connector.Core.Context;
using Microsoft.Extensions.DependencyInjection;
using Dataverse.Http.Connector.Core.Persistence;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;

namespace Dataverse.Http.Connector.Core
{
    /// <summary>
    /// This class implements the principal functions to work with Dataverse connector core library.
    /// </summary>
    public class DataverseContext : IDataverseContext
    {
        /// <summary>
        /// Private Dataverse builder service instance.
        /// </summary>
        private readonly IDataverseBuilder _builder;

        /// <summary>
        /// Private application service provider instance.
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        /// Initialize new "DataverseContext" service.
        /// </summary>
        /// <param name="builder">Dataverse builder service instance.</param>
        public DataverseContext(IDataverseBuilder builder, IServiceProvider provider)
        {
            _builder = builder;
            _provider = provider;
        }

        /// <summary>
        /// This function set the connection to an environment using connection name.
        /// </summary>
        /// <param name="name">Dataverse connection name.</param>
        public void SetEnvironment(string name)
            => _builder.ChangeEnvironmentConnection(_builder.Connections.FirstOrDefault(x => x.ConnectionName == name));

        /// <summary>
        /// This function set the connection to an environment using connection unique identifier.
        /// </summary>
        /// <param name="id">Dataverse connection unique identifier.</param>
        public void SetEnvironment(Guid id)
            => _builder.ChangeEnvironmentConnection(_builder.Connections.FirstOrDefault(x => x.ConnectionId == id));

        /// <summary>
        /// This function set the connection to an environment using connection instance.
        /// </summary>
        /// <param name="connection">Dataverse connection instance.</param>
        public void SetEnvironment(DataverseConnection connection)
            => _builder.ChangeEnvironmentConnection(_builder.Connections.FirstOrDefault(connection));

        /// <summary>
        /// Function to initialize a new "DbEntitySet" to connect the API and get information.
        /// </summary>
        /// <typeparam name="TEntity">Entity type class.</typeparam>
        /// <returns>DbEntitySet with type of entity class.</returns>
        /// <exception cref="EntityDefinitionException">The TEntity type was not found in the context.</exception>
        public virtual IDbEntitySet<TEntity> Set<TEntity>() where TEntity : class, new()
        {
            if (!_builder.Entities.Any(x => x.EntityType == typeof(TEntity)))
                throw new EntityDefinitionException(typeof(TEntity).Name, typeof(TEntity));
            return _provider.GetRequiredService<IDbEntitySet<TEntity>>();
        }
    }
}
