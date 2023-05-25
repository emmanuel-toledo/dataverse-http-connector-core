using Dynamics.Crm.Http.Connector.Core.Models.Context;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Newtonsoft.Json.Linq;

namespace  Dynamics.Crm.Http.Connector.Core.Persistence
{
    /// <summary>
    /// This interface defines the principal functions to work with Dynamics connector core library.
    /// </summary>
    public interface IDynamicsContext
    {
        /// <summary>
        /// This function set the connection to an environment using connection name.
        /// </summary>
        /// <param name="name">Dynamics connection name.</param>
        void SetEnvironment(string name);

        /// <summary>
        /// This function set the connection to an environment using connection unique identifier.
        /// </summary>
        /// <param name="id">Dynamics connection unique identifier.</param>
        void SetEnvironment(Guid id);

        /// <summary>
        /// This function set the connection to an environment using connection instance.
        /// </summary>
        /// <param name="connection">Dynamics connection instance.</param>
        void SetEnvironment(DynamicsConnection connection);

        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="id">Entity record unique identifier.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> RetriveById(string schemaName, Guid id);

        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="fetchXml">FetchXml query string.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> RetriveByFetch(string schemaName, string fetchXml);

        /// <summary>
        /// Function to get information using a OData query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="oData">OData query string.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> RetriveByOData(string schemaName, string oData);

        /// <summary>
        /// Function to initialize a new "DbEntitySet" to connect the API and get information.
        /// </summary>
        /// <typeparam name="TEntity">Entity type class.</typeparam>
        /// <returns>DbEntitySet with type of entity class.</returns>
        DbEntitySet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
