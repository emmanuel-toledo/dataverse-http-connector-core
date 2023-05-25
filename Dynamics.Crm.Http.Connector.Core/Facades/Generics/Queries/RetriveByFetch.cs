using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Facades.Generics.Queries
{
    /// <summary>
    /// This interface defines service to retrive information of an entity using FetchXml query.
    /// </summary>
    internal interface IRetriveByFetch
    {
        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="fetchXml">FetchXml query string.</param>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> SendAsync(string schemaName, string fetchXml);
    }

    /// <summary>
    /// This class implements service to retrive information of an entity using FetchXml query.
    /// </summary>
    internal class RetriveByFetch : IRetriveByFetch
    {
        /// <summary>
        /// Private Dynamics request service instance.
        /// </summary>
        private readonly IDynamicsRequest _dynamics;

        /// <summary>
        /// Initialize a new instance of "RetriveByOData" service.
        /// </summary>
        /// <param name="dynamics">Dynamics request service instance.</param>
        public RetriveByFetch(IDynamicsRequest dynamics)
            => _dynamics = dynamics;

        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="fetchXml">FetchXml query string.</param>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> SendAsync(string schemaName, string fetchXml)
            => await _dynamics.SendAsync(new HttpRequestMessage(method: HttpMethod.Get, $"{schemaName}?fetchXml={fetchXml}"), true);
    }
}
