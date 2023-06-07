using Dynamics.Crm.Http.Connector.Core.Business.Handler;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context;
using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Business.Commands
{
    /// <summary>
    /// This class implements the function to request commands to Dynamics.
    /// </summary>
    public class DynamicsCommands : IDynamicsCommands
    {
        private readonly IDynamicsRequest _dynamics;

        private readonly IParseHandler _parseHandler;

        private readonly IDynamicsBuilder _builder;

        public DynamicsCommands(IDynamicsRequest dynamics, IParseHandler parseHandler, IDynamicsBuilder builder)
        {
            _dynamics = dynamics;
            _parseHandler = parseHandler;
            _builder = builder;
        }

        /// <summary>
        /// Function to create a new entity record in Dynamics.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="requestMessage">Message configuration to HTTP request.</param>
        /// <param name="entity">TEntity object to HTTP request.</param>
        /// <returns>New TEntity instance or null value.</returns>
        public async Task<TEntity?> AddAsync<TEntity>(HttpRequestMessage requestMessage, TEntity entity) where TEntity : class, new()
        {
            // Parse TEntity to JSON Object and set content to request.
            var model = _parseHandler.ParseTEntityToModel(entity);
            requestMessage.Content = JsonContent.Create(model);
            // Send async request to Dynamics.
            var response = await _dynamics.SendAsync(requestMessage, false);
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
                    entity.GetType().GetProperty(nameof(fieldAttribute.TEntityPropertyName))!.SetValue(entity, new Guid(values.First().Substring(values.First().Length - 37, 36)));
                }
            }
            return entity;
        }
    }
}
