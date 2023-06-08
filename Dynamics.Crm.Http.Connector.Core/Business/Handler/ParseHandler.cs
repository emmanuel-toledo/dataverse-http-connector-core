using Newtonsoft.Json.Linq;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;
using Dynamics.Crm.Http.Connector.Core.Extensions.Utilities;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dynamics.Crm.Http.Connector.Core.Business.Handler
{
    /// <summary>
    /// This interface defines the methdos to parse a Json response inside a TEntity class.
    /// </summary>
    public interface IParseHandler
    {
        /// <summary>
        /// Function to parse a collection records inside a TEntity collection.
        /// </summary>
        /// <typeparam name="TEntity">Configured entity class.</typeparam>
        /// <param name="jsonArray">Json object collection.</param>
        /// <returns>Collection of TEntity class.</returns>
        ICollection<TEntity> ParseModelToTEntityCollection<TEntity>(JArray jsonArray) where TEntity : class, new();

        /// <summary>
        /// Function to parse a single Json object inside a TEntity object
        /// </summary>
        /// <typeparam name="TEntity">Configured entity class.</typeparam>
        /// <param name="jsonObject">Json object.</param>
        /// <returns>Instance of TEntity class.</returns>
        TEntity? ParseModelToTEntity<TEntity>(JToken jsonObject) where TEntity : class, new();

        /// <summary>
        /// Function to parse a TEntity record to an JSON Object.
        /// </summary>
        /// <typeparam name="TEntity">Configured entity class.</typeparam>
        /// <param name="entity">TEntity object instance.</param>
        /// <returns>JSON Object to request.</returns>
        JObject? ParseTEntityToModel<TEntity>(TEntity entity) where TEntity : class, new();
    }

    /// <summary>
    /// This class implements the methdos to parse a Json response inside a TEntity class.
    /// </summary>
    internal class ParseHandler : IParseHandler
    {
        /// <summary>
        /// Private IDynamicsBuilder service.
        /// </summary>
        private readonly IDynamicsBuilder _builder;

        /// <summary>
        /// Initialize a new instance of ParseHandler service.
        /// </summary>
        /// <param name="builder">IDynamicsBuilder service.</param>
        public ParseHandler(IDynamicsBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Function to parse a collection records inside a TEntity collection.
        /// </summary>
        /// <typeparam name="TEntity">Configured entity class.</typeparam>
        /// <param name="jsonArray">Json object collection.</param>
        /// <returns>Collection of TEntity class.</returns>
        public ICollection<TEntity> ParseModelToTEntityCollection<TEntity>(JArray jsonArray) where TEntity : class, new()
        {
            try
            {
                // Validate if entity exists inside the Builder configuration.
                var entityType = new TEntity().GetType();
                if (!_builder.Entities.Any(x => x.EntityType == entityType))
                    throw new EntityDefinitionException(entityType.Name, entityType);
                // Loop inside the response array.
                var collection = new List<TEntity>();
                foreach (var jsonObject in jsonArray)
                {
                    if (jsonObject is null)
                        continue;
                    // Parse each record to entity model.
                    var entity = ParseModelToTEntity<TEntity>(jsonObject);
                    if (entity != null)
                        collection.Add(entity);
                }
                return collection;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException("An error has ocurred during the convertion to the custom class.", ex);
            }
        }

        /// <summary>
        /// Function to parse a single Json object inside a TEntity object
        /// </summary>
        /// <typeparam name="TEntity">Configured entity class.</typeparam>
        /// <param name="jsonObject">Json object.</param>
        /// <returns>Instance of TEntity class.</returns>
        public TEntity? ParseModelToTEntity<TEntity>(JToken jsonObject) where TEntity : class, new()
        {
            try
            {
                // Validate JSON object and initialize a new instance of TEntity.
                if (jsonObject is null)
                    return null;
                var entity = new TEntity();
                var entityType = entity.GetType();
                // Validate if entity exists inside the Builder configuration.
                if (!_builder.Entities.Any(x => x.EntityType == entityType))
                    throw new EntityDefinitionException(entityType.Name, entityType);
                var entityDeffinition = _builder.Entities.First(x => x.EntityType == entityType);
                // Loop of all fields attributes of the entity.
                foreach (var property in entityType.GetProperties())
                {
                    try
                    {
                        // Validate if class property exists inside the entity deffinition properties.
                        if (!entityDeffinition.FieldsAttributes.Any(x => x.TEntityPropertyName == property.Name))
                            continue;
                        var fieldAttribute = entityDeffinition.FieldsAttributes.First(x => x.TEntityPropertyName == property.Name);
                        // Set default value if property of JSON does not exists.
                        var propertyValue = jsonObject.Value<string>(fieldAttribute.SchemaName!);
                        if (string.IsNullOrEmpty(propertyValue))
                        {
                            property.SetValue(entity, default, null);
                            continue;
                        }
                        // Check field type to parse information.
                        switch (fieldAttribute.FieldType)
                        {
                            case FieldTypes.Text:
                            case FieldTypes.Number:
                            case FieldTypes.DecimalNumber:
                            case FieldTypes.Lookup:
                            case FieldTypes.OptionSet:
                            case FieldTypes.BoolOptionSet:
                            case FieldTypes.UniqueIdentifier:
                                property.SetTEntityPropertyValue(entity, fieldAttribute, jsonObject);
                                break;
                            case FieldTypes.DateTime:
                                property.SetValue(entity, jsonObject.Value<DateTime>(fieldAttribute.SchemaName!), null);
                                break;
                            default:
                                property.SetValue(entity, default, null);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new NotSupportedException($"An error has ocurred during the data conversion of property '{property.Name}' of type '{property.PropertyType.Name}' for class '{entityType.Name}'. It does has the correct property type?", ex);
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException("An error has ocurred during the convertion to the custom class.", ex);
            }
        }

        /// <summary>
        /// Function to parse a TEntity record to an JSON Object.
        /// </summary>
        /// <typeparam name="TEntity">Configured entity class.</typeparam>
        /// <param name="entity">TEntity object instance.</param>
        /// <returns>JSON Object to request.</returns>
        public JObject? ParseTEntityToModel<TEntity>(TEntity entity) where TEntity : class, new()
        {
            try
            {
                // Validate if entity is null.
                if (entity is null)
                    return null;
                // Validate if entity exists inside the Builder configuration.
                if (!_builder.Entities.Any(x => x.EntityType == entity.GetType()))
                    throw new EntityDefinitionException(entity.GetType().Name, entity.GetType());
                var entityDeffinition = _builder.Entities.First(x => x.EntityType == entity.GetType());
                // Generate JSON object.
                var model = new JObject();
                // Loop of all fields attributes of the entity.
                foreach (var property in entity.GetType().GetProperties())
                {
                    try
                    {
                        // Validate if class property exists inside the entity deffinition properties.
                        if (!entityDeffinition.FieldsAttributes.Any(x => x.TEntityPropertyName == property.Name))
                            continue;
                        var fieldAttribute = entityDeffinition.FieldsAttributes.First(x => x.TEntityPropertyName == property.Name);
                        // Validate if field attribute is entity's unique identifier.
                        if (fieldAttribute.FieldType == FieldTypes.UniqueIdentifier)
                            continue;
                        // Check field type to parse information.
                        switch (fieldAttribute.FieldType)
                        {
                            case FieldTypes.Text:
                            case FieldTypes.Number:
                            case FieldTypes.DecimalNumber:
                            case FieldTypes.OptionSet:
                            case FieldTypes.BoolOptionSet:
                                model.Add(fieldAttribute.LogicalName!, property.GetTEntityPropertyValue(entity));
                                break;
                            case FieldTypes.Lookup:
                                model.Add($"{fieldAttribute.LogicalName!}@odata.bind", $"/{fieldAttribute.LinkedEntityLogicalCollectionName}({property.GetTEntityPropertyValue(entity)})");
                                break;
                            case FieldTypes.DateTime:
                                var value = property.GetTEntityPropertyValue(entity);
                                var datetime = DateTime.Parse(value!);
                                model.Add(fieldAttribute.LogicalName!, datetime.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new NotSupportedException($"An error has ocurred during the data conversion of property '{property.Name}' of type '{property.PropertyType.Name}' for class '{entity.GetType().Name}'. It does has the correct property type?", ex);
                    }
                }
                return model;
            } 
            catch(Exception ex)
            {
                throw new NotSupportedException("An error has ocurred during the convertion the custom class to JSON Object.", ex);
            }
        }
    }
}
