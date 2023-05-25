using  Dynamics.Crm.Http.Connector.Core.Persistence;
using Dynamics.Crm.Http.Connector.Core.Models.Context;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;

namespace  Dynamics.Crm.Http.Connector.Core
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
        /// Initialize new "DynamicsContext" service.
        /// </summary>
        /// <param name="builder">Dynamics builder service instance.</param>
        public DynamicsContext(IDynamicsBuilder builder)
            => _builder = builder;

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
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="id">Entity record unique identifier.</param>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> RetriveById(string schemaName, Guid id)
        {
            return new HttpResponseMessage();
        }

        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="fetchXml">FetchXml query string.</param>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> RetriveByFetch(string schemaName, string fetchXml)
        {
            return new HttpResponseMessage();
        }

        /// <summary>
        /// Function to get information using a OData query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="oData">OData query string.</param>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> RetriveByOData(string schemaName, string oData)
        {
            return new HttpResponseMessage();
        }

        /// <summary>
        /// Function to initialize a new "DbEntitySet" to connect the API and get information.
        /// </summary>
        /// <typeparam name="TEntity">Entity type class.</typeparam>
        /// <returns>DbEntitySet with type of entity class.</returns>
        public virtual DbEntitySet<TEntity> Set<TEntity>() where TEntity : class
        {
            var entityBuilder = _builder.Entities.FirstOrDefault(x => x.EntityType == typeof(TEntity));
            return entityBuilder is null
                ? throw new NotImplementedException($"The entity type '{typeof(TEntity).Name}' was not configured in context.")
                : (DbEntitySet<TEntity>)Activator.CreateInstance(typeof(DbEntitySet<TEntity>))!;
        }
    }
}
