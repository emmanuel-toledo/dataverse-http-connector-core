using Dataverse.Http.Connector.Core.Context;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;

namespace Dataverse.Http.Connector.Core.Persistence
{
    /// <summary>
    /// This interface defines the principal functions to work with Dataverse connector core library.
    /// </summary>
    public interface IDataverseContext
    {
        /// <summary>
        /// This function set the connection to an environment using connection name.
        /// </summary>
        /// <param name="name">Dataverse connection name.</param>
        void SetEnvironment(string name);

        /// <summary>
        /// This function set the connection to an environment using connection unique identifier.
        /// </summary>
        /// <param name="id">Dataverse connection unique identifier.</param>
        void SetEnvironment(Guid id);

        /// <summary>
        /// This function set the connection to an environment using connection instance.
        /// </summary>
        /// <param name="connection">Dataverse connection instance.</param>
        void SetEnvironment(DataverseConnection connection);

        /// <summary>
        /// Function to initialize a new "DbEntitySet" to connect the API and get information.
        /// </summary>
        /// <typeparam name="TEntity">Entity type class.</typeparam>
        /// <returns>DbEntitySet with type of entity class.</returns>
        IDbEntitySet<TEntity> Set<TEntity>() where TEntity : class, new();
    }
}
