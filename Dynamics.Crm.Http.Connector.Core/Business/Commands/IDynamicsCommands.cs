using Newtonsoft.Json.Linq;

namespace Dynamics.Crm.Http.Connector.Core.Business.Commands
{
    /// <summary>
    /// This interface defines the function to request commands to Dynamics.
    /// </summary>
    public interface IDynamicsCommands
    {
        /// <summary>
        /// Function to create a new entity record in Dynamics.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>New TEntity instance or null value.</returns>
        Task<TEntity?> AddAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new();
    }
}
