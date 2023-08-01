using System.Xml.Linq;
using System.Collections;
using Dataverse.Http.Connector.Core.Domains.Xml;
using Dataverse.Http.Connector.Core.Domains.Enums;
using Dataverse.Http.Connector.Core.Domains.Annotations;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dataverse.Http.Connector.Core.Utilities
{
    /// <summary>
    /// This class contains methods to build a new FetchXml query for Dataverse.
    /// </summary>
    internal static class FetchXmlBuilderUtilities
    {
        /// <summary>
        /// This function creates a new "condition" tag for a FetchXml query to be used to consume Dataverse service.
        /// </summary>
        /// <param name="model">Condition model instance.</param>
        /// <param name="isLogger">Create fetchXml with no values.</param>
        /// <returns>Condition tag in string format.</returns>
        /// <exception cref="ArgumentNullException">Condition model does not contains a valid Condition Type value.</exception>
        public static XElement CreateXmlCondition(Condition model, bool isLogger = false)
        {
            // Check if the condition does not have null value in required properties.
            Check.NotNullOrEmpty(model.Property, nameof(model.Property));
            Check.NotNull(model.ConditionType, nameof(model.ConditionType));
            // Create condition element.
            var xCondition = new XElement("condition");
            xCondition.SetAttributeValue("attribute", model.Property);
            xCondition.SetAttributeValue("operator", Conditions.Parse(model.ConditionType));
            // Evaluate each kind of condition type.
            switch (model.ConditionType)
            {
                case ConditionTypes.In:
                case ConditionTypes.NotIn:
                case ConditionTypes.Between:
                case ConditionTypes.NotBetween:
                    foreach (var value in (model.Value as IEnumerable)!)
                    {
                        var xValue = new XElement("value", isLogger ? $"@{model.Property!.ToUpper()}" : value);
                        xCondition.Add(xValue);
                    }
                    break;
                case ConditionTypes.Equal:
                case ConditionTypes.NotEqual:
                case ConditionTypes.BeginsWith:
                case ConditionTypes.DoesNotBeginWith:
                case ConditionTypes.EndsWith:
                case ConditionTypes.DoesNotEndsWith:
                case ConditionTypes.Like:
                case ConditionTypes.NotLike:
                case ConditionTypes.GreaterThan:
                case ConditionTypes.GreaterEqual:
                case ConditionTypes.LessThan:
                case ConditionTypes.LessEqual:
                    xCondition.SetAttributeValue("value", isLogger ? $"@{model.Property!.ToUpper()}" : model.Value);
                    break;
                default:
                    break;
            }
            return xCondition;
        }

        /// <summary>
        /// This function creates a new "filter" tag for a FetchXml query to be used to consume Dataverse service.
        /// <para>
        /// Already includes specified "condition" tags.
        /// </para>
        /// </summary>
        /// <param name="model">Filter model instance.</param>
        /// <param name="isLogger">Create fetchXml with no values.</param>
        /// <returns>Filter tag in string format.</returns>
        public static XElement CreateXmlFilter(Filter model, bool isLogger = false)
        {
            // Create filter element.
            var xFilter = new XElement("filter");
            xFilter.SetAttributeValue("type", Filters.Parse(model.FilterType));
            // Create each condition for xFilter.
            foreach (var condition in model.Conditions)
            {
                var xCondition = CreateXmlCondition(condition, isLogger);
                xFilter.Add(xCondition);
            }
            return xFilter;
        }

        /// <summary>
        /// Function to generate the FetchXml query for Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Model to cast query.</typeparam>
        /// <param name="model">FetchXml model instance.</param>
        /// <param name="entityAttributes">Entity attributes of TEntity class.</param>
        /// <param name="columnsAttributes">Column attributes collection of TEntity class.</param>
        /// <param name="isLogger">Create fetchXml with no values.</param>
        /// <returns>FetchXml query.</returns>
        public static string CreateEntityFetchXmlQuery<TEntity>(FetchXml model, Entity entityAttributes, ICollection<Column> columnsAttributes, bool isLogger = false) where TEntity : class, new()
        {
            // Create new XML for query document.
            var xDocument = new XDocument();
            // Create main element.
            var xFetch = new XElement("fetch");
            xFetch.SetAttributeValue("version", 1.0);
            xFetch.SetAttributeValue("mapping", "logical");
            xFetch.SetAttributeValue("no-lock", model.NoLock);
            if (model.Top > 0)
                xFetch.SetAttributeValue("top", model.Top);
            if (model.PageSize > 0)
                xFetch.SetAttributeValue("count", model.PageSize);
            if (model.Page > 0)
                xFetch.SetAttributeValue("page", model.Page);
            if (model.Distinct)
                xFetch.SetAttributeValue("distinct", model.Distinct.ToString().ToLower());
            // Add entity element.
            var xEntity = new XElement("entity");
            xEntity.SetAttributeValue("name", entityAttributes.LogicalName);
            // Add all attributes to retrive.
            foreach (var column in columnsAttributes)
            {
                var xAttribute = new XElement("attribute");
                xAttribute.SetAttributeValue("name", column.LogicalName);
                if(!isLogger)
                    xAttribute.SetAttributeValue("alias", Parse.RemoveSpecialCharacters(column.TEntityPropertyName).ToUpper());
                xEntity.Add(xAttribute);
            }
            // Add filters and conditions.
            foreach (var filter in model.Filters)
            {
                var xFilter = CreateXmlFilter(filter, isLogger);
                xEntity.Add(xFilter);
            }
            // Set elements to XML document query.
            xFetch.Add(xEntity);
            xDocument.Add(xFetch);
            return xDocument.ToString();
        }

        /// <summary>
        /// Function to generate the FetchXml query to count records in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Model to cast query.</typeparam>
        /// <param name="model">FetchXml model instance.</param>
        /// <param name="entityAttributes">Entity attributes of TEntity class.</param>
        /// <param name="columnsAttributes">Column attributes collection of TEntity class.</param>
        /// <returns>FetchXml query.</returns>
        public static string CreateCountFetchXmlQuery<TEntity>(FetchXml model, Entity entityAttributes, ICollection<Column> columnsAttributes) where TEntity : class, new()
        {
            // Create new XML for query document.
            var xDocument = new XDocument();
            // Create main element.
            var xFetch = new XElement("fetch");
            xFetch.SetAttributeValue("distinct", model.Distinct.ToString().ToLower());
            xFetch.SetAttributeValue("aggregate", true);
            // Add entity element.
            var xEntity = new XElement("entity");
            xEntity.SetAttributeValue("name", entityAttributes.LogicalName);
            // Validate if the class contains a unique identifier property.
            if (!columnsAttributes.Any(x => x.ColumnType == ColumnTypes.UniqueIdentifier))
                throw new EntityDefinitionException(entityAttributes.LogicalName!, ColumnTypes.UniqueIdentifier);
            // Add attribute to FetchXml.
            var column = columnsAttributes.First(x => x.ColumnType == ColumnTypes.UniqueIdentifier);
            var xAttribute = new XElement("attribute");
            xAttribute.SetAttributeValue("name", column.LogicalName);
            xAttribute.SetAttributeValue("alias", "CountRecords");
            xAttribute.SetAttributeValue("aggregate", Aggregates.Parse(AggregateTypes.COUNT));
            xEntity.Add(xAttribute);
            // Add filters and conditions.
            foreach (var filter in model.Filters)
            {
                var xFilter = CreateXmlFilter(filter);
                xEntity.Add(xFilter);
            }
            // Set elements to XML document query.
            xFetch.Add(xEntity);
            xDocument.Add(xFetch);
            return xDocument.ToString();
        }
    }
}
