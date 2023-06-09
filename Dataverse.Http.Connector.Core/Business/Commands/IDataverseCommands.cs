namespace Dataverse.Http.Connector.Core.Business.Commands
{
    /// <summary>
    /// This interface defines the function to request commands to Dataverse.
    /// </summary>
    public interface IDataverseCommands
    {
        /// <summary>
        /// Function to create a new entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>New TEntity instance or null value.</returns>
        Task<TEntity?> AddAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Function to updated an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>TEntity instance or null value.</returns>
        Task<TEntity?> UpdateAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Function to delete an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>TEntity instance or null value.</returns>
        Task<TEntity?> DeleteAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new();
    }
}
