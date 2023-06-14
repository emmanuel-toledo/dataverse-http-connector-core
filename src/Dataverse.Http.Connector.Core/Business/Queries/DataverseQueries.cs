using Newtonsoft.Json.Linq;
using Dataverse.Http.Connector.Core.Facades.Requests;
using Dataverse.Http.Connector.Core.Business.Handler;
using Dataverse.Http.Connector.Core.Extensions.Utilities;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse;

namespace Dataverse.Http.Connector.Core.Business.Queries
{
    /// <summary>
    /// This class implements the function to request querys to Dataverse.
    /// </summary>
    internal class DataverseQueries : IDataverseQueries
    {
        /// <summary>
        /// Private dataverse request service.
        /// </summary>
        private readonly IDataverseRequest _dataverse;
        
        /// <summary>
        /// Private parse handler service.
        /// </summary>
        private readonly IParseHandler _parseHandler;

        /// <summary>
        /// Initialize a new instance of Dataverse queries service.
        /// </summary>
        /// <param name="dataverse">Dataverse request service.</param>
        /// <param name="parseHandler">Parse handler service.</param>
        public DataverseQueries(IDataverseRequest dataverse, IParseHandler parseHandler)
        {
            _dataverse = dataverse;
            _parseHandler = parseHandler;
        }

        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "Entity" and "Field" attributes defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>TEntity record instance.</returns>
        /// <exception cref="NotFoundException">The requested resource was not found.</exception>
		public async Task<TEntity> FirstAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new()
        {
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
            // If is not success return a null value.
            if (!response.IsSuccessStatusCode)
                response.ThrowDataverseException();
            // Convert response to JObject.
            var contentResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
            var content = contentResponse.Value<JArray>("value");
            if (content is null || content.Count <= 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.ThrowDataverseException();
            }
            // Parse content to model.
            var entity = _parseHandler.ParseModelToTEntity<TEntity>(content!.FirstOrDefault()!);
            return entity!;
        }

        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "Entity" and "Field" attributes defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>TEntity record instance.</returns>
        public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new()
        {
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
            // If is not success return a null value.
            if (!response.IsSuccessStatusCode)
                return null;
            // Convert response to JObject.
            var contentResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
            var content = contentResponse.Value<JArray>("value");
            if (content is null || content.Count <= 0)
                return null;
            // Parse content to model.
            var entity = _parseHandler.ParseModelToTEntity<TEntity>(content.FirstOrDefault()!);
            return entity;
        }

        /// <summary>
        /// Function to retrive a collection of "TEntity" records according to defined FetchXml query.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "Entity" and "Field" attributes defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>Collection of TEntity.</returns>
		public async Task<ICollection<TEntity>> ToListAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new()
        {
            var collection = new List<TEntity>();
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
			// If is not success return a null value.
			if (!response.IsSuccessStatusCode)
				return collection;
			// Convert response to JObject.
			var contentResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
			var content = contentResponse.Value<JArray>("value");
			if (content is null || content.Count <= 0)
				return collection;
            // Parse content to model.
            collection = _parseHandler.ParseModelToTEntityCollection<TEntity>(content).ToList();
            return collection;
		}

        /// <summary>
        /// Function to retrive a count of records of an entity in Dataverse.
        /// </summary>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <returns>Records count.</returns>
        public async Task<int> CountAsync(HttpRequestMessage requestMessage)
        {
            int count = default;
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
            // If is not success return a null value.
            if (!response.IsSuccessStatusCode)
                return count;
            // Convert response to JObject.
            var contentResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
            var content = contentResponse.Value<JArray>("value");
            if (content is null || content.Count <= 0)
                return count;
            // Return count response.
            foreach (var item in content)
                count = item.Value<int>("CountRecords");
            return count;
        }
    }
}
