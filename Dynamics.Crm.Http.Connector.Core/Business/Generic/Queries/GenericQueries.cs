using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Business.Generic.Queries
{
    internal class GenericQueries : IGenericQueries
    {
        /// <summary>
        /// Function to get Dynamics Entities deffinitions.
        /// </summary>
        /// <returns>Http response message object.</returns>
        public Task<HttpResponseMessage> EntitiesDeffinitionsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to get entity record information using a unique identifier.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="id">Entity record unique identifier.</param>
        /// <returns>Http response message object.</returns>
        public Task<HttpResponseMessage> RetriveByIdAsync(string schemaName, Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to get information using a fetchXml query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="fetchXml">FetchXml query string.</param>
        /// <returns>Http response message object.</returns>
        public Task<HttpResponseMessage> RetriveByFetchAsync(string schemaName, string fetchXml)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to get information using a OData query.
        /// </summary>
        /// <param name="schemaName">Entity schema name.</param>
        /// <param name="oData">OData query string.</param>
        /// <returns>Http response message object.</returns>
        public Task<HttpResponseMessage> RetriveByODataAsync(string schemaName, string oData)
        {
            throw new NotImplementedException();
        }
    }
}
