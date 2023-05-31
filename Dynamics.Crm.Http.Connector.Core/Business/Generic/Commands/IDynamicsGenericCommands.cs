namespace Dynamics.Crm.Http.Connector.Core.Business.Generic.Commands
{
    internal interface IDynamicsGenericCommands
    {
        /// <summary>
        /// Function to create a record of an entity using a custom Http request message.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="request"></param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> CreateAsync(string schemaName, HttpRequestMessage request);

        /// <summary>
        /// Function to update a record of an entity using an unique identifier and custom Http request message.
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> UpdateAsync(string schemaName, Guid id, HttpRequestMessage request);

        /// <summary>
        /// Function to delete a record of an entity using an unique identifier.
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="id"></param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> DeleteAsync(string schemaName, Guid id);
    }
}
