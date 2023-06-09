namespace Dataverse.Http.Connector.Core.Business.Queries
{
    /// <summary>
    /// This interface defines the function to request querys to Dataverse.
    /// </summary>
    public interface IDataverseQueries
    {
        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>TEntity record instance.</returns>
		Task<TEntity> FirstAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new();

        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>TEntity record instance.</returns>
		Task<TEntity?> FirstOrDefaultAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new();

        /// <summary>
        /// Function to retrive a collection of "TEntity" records according to defined FetchXml query.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>Collection of TEntity.</returns>
		Task<ICollection<TEntity>> ToListAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new();

        /// <summary>
        /// Function to retrive a count of records of an entity in Dataverse.
        /// </summary>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>Records count.</returns>
        Task<int> CountAsync(HttpRequestMessage requestMessage);
    }
}
