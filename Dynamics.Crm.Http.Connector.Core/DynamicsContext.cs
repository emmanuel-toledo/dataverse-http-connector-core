using Microsoft.Extensions.DependencyInjection;
using Dynamics.Crm.Http.Connector.Core.Context;
using Dynamics.Crm.Http.Connector.Core.Persistence;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Connection;

namespace Dynamics.Crm.Http.Connector.Core
{
    /// <summary>
    /// This class implements the principal functions to work with Dynamics connector core library.
    /// </summary>
    public class DynamicsContext : IDynamicsContext
    {
        /// <summary>
        /// Private dynamics builder service instance.
        /// </summary>
        private readonly IDynamicsBuilder _builder;

        /// <summary>
        /// Private application service provider instance.
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        /// Initialize new "DynamicsContext" service.
        /// </summary>
        /// <param name="builder">Dynamics builder service instance.</param>
        public DynamicsContext(IDynamicsBuilder builder, IServiceProvider provider)
        {
            _builder = builder;
            _provider = provider;
        }

        /// <summary>
        /// This function set the connection to an environment using connection name.
        /// </summary>
        /// <param name="name">Dynamics connection name.</param>
        public void SetEnvironment(string name)
            => _builder.ChangeEnvironmentConnection(_builder.Connections.FirstOrDefault(x => x.ConnectionName == name));

        /// <summary>
        /// This function set the connection to an environment using connection unique identifier.
        /// </summary>
        /// <param name="id">Dynamics connection unique identifier.</param>
        public void SetEnvironment(Guid id)
            => _builder.ChangeEnvironmentConnection(_builder.Connections.FirstOrDefault(x => x.ConnectionId == id));

        /// <summary>
        /// This function set the connection to an environment using connection instance.
        /// </summary>
        /// <param name="connection">Dynamics connection instance.</param>
        public void SetEnvironment(DynamicsConnection connection)
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
