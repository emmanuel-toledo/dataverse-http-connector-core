using System.Text;
using System.Net.Http.Headers;
using Dataverse.Http.Connector.Core.Facades.Requests;
using Dataverse.Http.Connector.Core.Business.Handler;
using Dataverse.Http.Connector.Core.Domains.Annotations;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;

namespace Dataverse.Http.Connector.Core.Business.Commands
{
    /// <summary>
    /// This class implements the function to request commands to Dataverse.
    /// </summary>
    internal class DataverseCommands : IDataverseCommands
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
        /// Private dataverse builder.
        /// </summary>
        private readonly IDataverseBuilder _builder;

        /// <summary>
        /// Initialize a new instance of Dataverse commands service.
        /// </summary>
        /// <param name="dataverse">Dataverse request service.</param>
        /// <param name="parseHandler">Parse hander service.</param>
        /// <param name="builder">Dataverse builder service.</param>
        public DataverseCommands(IDataverseRequest dataverse, IParseHandler parseHandler, IDataverseBuilder builder)
        {
            _dataverse = dataverse;
            _parseHandler = parseHandler;
            _builder = builder;
        }

        /// <summary>
        /// Function to create a new entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "Entity" and "Field" attributes defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>New TEntity instance or null value.</returns>
        public async Task<TEntity?> AddAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new()
        {
            // Parse TEntity to JSON Object and set content to request.
            var model = _parseHandler.ParseTEntityToModel(entity);
            if (model is null)
                return null;
            requestMessage.Content = new StringContent(model.ToString(), Encoding.UTF8, "application/json");
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
            // If is not success return a null value.
            if (!response.IsSuccessStatusCode)
                return null;
            HttpHeaders headers = response.Headers;
            if (!headers.TryGetValues("OData-EntityId", out IEnumerable<string>? values))
                return null;
            // Set value to unique identifer if exists.
            if(_builder.Entities.Any(x => x.EntityType == entity.GetType()))
            {
                var entityAttribute = _builder.Entities.First(x => x.EntityType == entity.GetType());
                if(entityAttribute.FieldsAttributes.Any(x => x.FieldType == FieldTypes.UniqueIdentifier))
                {
                    var fieldAttribute = entityAttribute.FieldsAttributes.First(x => x.FieldType == FieldTypes.UniqueIdentifier);
                    entity.GetType().GetProperty(fieldAttribute.TEntityPropertyName)!.SetValue(entity, new Guid(values.First().Substring(values.First().Length - 37, 36)));
                }
            }
            return entity;
        }

        /// <summary>
        /// Function to updated an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "Entity" and "Field" attributes defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>TEntity instance or null value.</returns>
        public async Task<TEntity?> UpdateAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new()
        {
            // Parse TEntity to JSON Object and set content to request.
            var model = _parseHandler.ParseTEntityToModel(entity);
            if (model is null)
                return null;
            requestMessage.Content = new StringContent(model.ToString(), Encoding.UTF8, "application/json");
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
            // If is not success return a null value.
            if (!response.IsSuccessStatusCode)
                return null;
            return entity;
        }

        /// <summary>
        /// Function to delete an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "Entity" and "Field" attributes defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>TEntity instance or null value.</returns>
        public async Task<TEntity?> DeleteAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new()
        {
            // Parse TEntity to JSON Object and set content to request.
            var model = _parseHandler.ParseTEntityToModel(entity);
            if (model is null)
                return null;
            // Send async request to Dataverse.
            var response = await _dataverse.SendAsync(requestMessage, false);
            // If is not success return a null value.
            if (!response.IsSuccessStatusCode)
                return null;
            return entity;
        }
    }
}
