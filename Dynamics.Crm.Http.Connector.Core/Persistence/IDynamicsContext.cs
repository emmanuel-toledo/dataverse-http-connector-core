using Newtonsoft.Json.Linq;
using Dynamics.Crm.Http.Connector.Core.Context;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Connection;

namespace Dynamics.Crm.Http.Connector.Core.Persistence
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
        /// Function to initialize a new "DbEntitySet" to connect the API and get information.
        /// </summary>
        /// <typeparam name="TEntity">Entity type class.</typeparam>
        /// <returns>DbEntitySet with type of entity class.</returns>
        IDbEntitySet<TEntity> Set<TEntity>() where TEntity : class, new();
    }
}
