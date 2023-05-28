namespace Dynamics.Crm.Http.Connector.Core.Business.Generic.Queries
{
    /// <summary>
    /// Interface to define Generic Queries to consume Dynamics CRM REST API.
    /// </summary>
    internal interface IGenericQueries
    {
        /// <summary>
        /// Function to get Dynamics Entities deffinitions.
        /// </summary>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> EntitiesDeffinitionsAsync();

        /// <summary>
        /// Function to get entity record information using a unique identifier.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="id">Entity record unique identifier.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> RetriveByIdAsync(string schemaName, Guid id);

        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="fetchXml">FetchXml query string.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> RetriveByFetchAsync(string schemaName, string fetchXml);

        /// <summary>
        /// Function to get information using a OData query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="oData">OData query string.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> RetriveByODataAsync(string schemaName, string oData);
    }
}
