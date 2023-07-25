using Newtonsoft.Json.Linq;
using Dataverse.Http.Connector.Core.Domains.Annotations;
using Dataverse.Http.Connector.Core.Extensions.Utilities;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions;
using Dataverse.Http.Connector.Core.Utilities;

namespace Dataverse.Http.Connector.Core.Business.Handler
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
        /// Private IDataverseBuilder service.
        /// </summary>
        private readonly IDataverseBuilder _builder;

        /// <summary>
        /// Initialize a new instance of ParseHandler service.
        /// </summary>
        /// <param name="builder">IDataverseBuilder service.</param>
        public ParseHandler(IDataverseBuilder builder)
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
                var entityDefinition = _builder.Entities.First(x => x.EntityType == entityType);
                // Loop of all columns attributes of the entity.
                foreach (var property in entityType.GetProperties())
                {
                    try
                    {
                        // Validate if class property exists inside the entity definition properties.
                        if (!entityDefinition.ColumnsAttributes.Any(x => x.TEntityPropertyName == property.Name))
                            continue;
                        var columnAttribute = entityDefinition.ColumnsAttributes.First(x => x.TEntityPropertyName == property.Name);
                        // Set default value if property of JSON does not exists.
                        var propertyValue = jsonObject.Value<string>(
                            Parse.RemoveSpecialCharacters(columnAttribute.TEntityPropertyName).ToUpper()
                        );
                        if (string.IsNullOrEmpty(propertyValue))
                        {
                            property.SetValue(entity, default, null);
                            continue;
                        }
                        // Check column type to parse information.
                        switch (columnAttribute.ColumnType)
                        {
                            case ColumnTypes.Text:
                            case ColumnTypes.Number:
                            case ColumnTypes.DecimalNumber:
                            case ColumnTypes.Lookup:
                            case ColumnTypes.OptionSet:
                            case ColumnTypes.BoolOptionSet:
                            case ColumnTypes.UniqueIdentifier:
                                property.SetTEntityPropertyValue(entity, columnAttribute, jsonObject);
                                break;
                            case ColumnTypes.DateTime:
                                property.SetValue(
                                    entity, 
                                    jsonObject.Value<DateTime>(Parse.RemoveSpecialCharacters(columnAttribute.TEntityPropertyName).ToUpper()), 
                                    null
                                );
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
                var entityDefinition = _builder.Entities.First(x => x.EntityType == entity.GetType());
                // Generate JSON object.
                var model = new JObject();
                // Loop of all columns attributes of the entity.
                foreach (var property in entity.GetType().GetProperties())
                {
                    try
                    {
                        // Validate if class property exists inside the entity definition properties.
                        if (!entityDefinition.ColumnsAttributes.Any(x => x.TEntityPropertyName == property.Name))
                            continue;
                        var columnAttribute = entityDefinition.ColumnsAttributes.First(x => x.TEntityPropertyName == property.Name);
                        // Validate if column attribute is entity's unique identifier.
                        if (columnAttribute.ColumnType == ColumnTypes.UniqueIdentifier)
                            continue;
                        // Check column type to parse information.
                        switch (columnAttribute.ColumnType)
                        {
                            case ColumnTypes.Text:
                            case ColumnTypes.Number:
                            case ColumnTypes.DecimalNumber:
                            case ColumnTypes.OptionSet:
                            case ColumnTypes.BoolOptionSet:
                                model.Add(columnAttribute.LogicalName!, property.GetTEntityPropertyValue(entity));
                                break;
                            case ColumnTypes.Lookup:
                                model.Add($"{columnAttribute.LogicalName!}@odata.bind", $"/{columnAttribute.LinkedEntityLogicalCollectionName}({property.GetTEntityPropertyValue(entity)})");
                                break;
                            case ColumnTypes.DateTime:
                                var value = property.GetTEntityPropertyValue(entity);
                                var datetime = DateTime.Parse(value!);
                                model.Add(columnAttribute.LogicalName!, datetime.ToString("yyyy-MM-ddTHH:mm:ssZ"));
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
