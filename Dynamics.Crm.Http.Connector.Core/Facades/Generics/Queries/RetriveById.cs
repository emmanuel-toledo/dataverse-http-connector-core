using Dynamics.Crm.Http.Connector.Core.Facades.Requests;

namespace Dynamics.Crm.Http.Connector.Core.Facades.Generics.Queries
{
    /// <summary>
    /// This interface defines service to retrive information of a specific record using a unique identifier.
    /// </summary>
    internal interface IRetriveById
    {
        /// <summary>
        /// Function to get entity record information using a unique identifier.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="id">Entity record unique identifier.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> SendAsync(string schemaName, Guid id);
    }

    /// <summary>
    /// This class implements service to retrive information of a specific record using a unique identifier.
    /// </summary>
    internal class RetriveById : IRetriveById
    {
        /// <summary>
        /// Private Dynamics request service instance.
        /// </summary>
        private readonly IDynamicsRequest _dynamics;

        /// <summary>
        /// Initialize a new instance of "RetriveByOData" service.
        /// </summary>
        /// <param name="dynamics">Dynamics request service instance.</param>
        public RetriveById(IDynamicsRequest dynamics)
            => _dynamics = dynamics;

        /// <summary>
        /// Function to get entity record information using a unique identifier.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="id">Entity record unique identifier.</param>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> SendAsync(string schemaName, Guid id)
            => await _dynamics.SendAsync(new HttpRequestMessage(method: HttpMethod.Get, $"{schemaName}({id})"), true);
    }
}
