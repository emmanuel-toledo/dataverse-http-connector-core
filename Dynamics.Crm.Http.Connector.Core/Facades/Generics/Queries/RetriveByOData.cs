using Dynamics.Crm.Http.Connector.Core.Facades.Requests;

namespace Dynamics.Crm.Http.Connector.Core.Facades.Generics.Queries
{
    /// <summary>
    /// This interface defines service to retrive information using oData query.
    /// </summary>
    internal interface IRetriveByOData
    {
        /// <summary>
        /// Function to get information using a OData query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="oData">OData query string.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> SendAsync(string schemaName, string oData);
    }

    /// <summary>
    /// This class implements service to retrive information using oData query.
    /// </summary>
    internal class RetriveByOData : IRetriveByOData
    {
        /// <summary>
        /// Private Dynamics request service instance.
        /// </summary>
        private readonly IDynamicsRequest _dynamics;

        /// <summary>
        /// Initialize a new instance of "RetriveByOData" service.
        /// </summary>
        /// <param name="dynamics">Dynamics request service instance.</param>
        public RetriveByOData(IDynamicsRequest dynamics)
            => _dynamics = dynamics;

        /// <summary>
        /// Function to get information using a OData query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="oData">OData query string.</param>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> SendAsync(string schemaName, string oData)
            => await _dynamics.SendAsync(new HttpRequestMessage(method: HttpMethod.Get, $"{schemaName}?{oData}"), true);
    }
}
