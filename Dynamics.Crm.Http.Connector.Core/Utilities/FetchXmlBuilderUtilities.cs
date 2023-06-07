﻿using System.Xml.Linq;
using System.Collections;
using Dynamics.Crm.Http.Connector.Core.Domains.Xml;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    /// <summary>
    /// This class contains methods to build a new FetchXml query for Dynamics.
    /// </summary>
    internal static class FetchXmlBuilderUtilities
    {
        /// <summary>
        /// This function creates a new "condition" tag for a FetchXml query to be used to consume Dynamics service.
        /// </summary>
        /// <param name="model">Condition model instance.</param>
        /// <returns>Condition tag in string format.</returns>
        /// <exception cref="ArgumentNullException">Condition model does not contains a valid Condition Type value.</exception>
        public static XElement CreateXmlCondition(Condition model)
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
                        var xValue = new XElement("value", value);
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
                    xCondition.SetAttributeValue("value", model.Value);
                    break;
                default:
                    break;
            }
            return xCondition;
        }

        /// <summary>
        /// This function creates a new "filter" tag for a FetchXml query to be used to consume Dynamics service.
        /// <para>
        /// Already includes specified "condition" tags.
        /// </para>
        /// </summary>
        /// <param name="model">Filter model instance.</param>
        /// <returns>Filter tag in string format.</returns>
        public static XElement CreateXmlFilter(Filter model)
        {
            // Create filter element.
            var xFilter = new XElement("filter");
            xFilter.SetAttributeValue("type", Filters.Parse(model.FilterType));
            // Create each condition for xFilter.
            foreach (var condition in model.Conditions)
            {
                var xCondition = CreateXmlCondition(condition);
                xFilter.Add(xCondition);
            }
            return xFilter;
        }

        /// <summary>
        /// Function to generate the FetchXml query for Dynamics.
        /// </summary>
        /// <typeparam name="TEntity">Model to cast query.</typeparam>
        /// <param name="model">FetchXml model instance.</param>
        /// <returns>FetchXml query.</returns>
        public static string CreateEntityFetchXmlQuery<TEntity>(FetchXml model, EntityAttributes entityAttributes, ICollection<FieldAttributes> fieldsAttributes) where TEntity : class, new()
        {
            // Create new XML for query document.
            var xDocument = new XDocument();
            // Create main element.
            var xFetch = new XElement("fetch");
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
            foreach (var field in fieldsAttributes)
            {
                var xAttribute = new XElement("attribute");
                xAttribute.SetAttributeValue("name", field.LogicalName);
                xAttribute.SetAttributeValue("alias", field.SchemaName);
                xEntity.Add(xAttribute);
            }
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

        /// <summary>
        /// Function to generate the FetchXml query to count records in Dynamics.
        /// </summary>
        /// <typeparam name="TEntity">Model to cast query.</typeparam>
        /// <param name="model">FetchXml model instance.</param>
        /// <returns>FetchXml query.</returns>
        public static string CreateCountFetchXmlQuery<TEntity>(FetchXml model, EntityAttributes entityAttributes, ICollection<FieldAttributes> fieldsAttributes) where TEntity : class, new()
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
            if (!fieldsAttributes.Any(x => x.FieldType == FieldTypes.UniqueIdentifier))
                throw new EntityDefinitionException(entityAttributes.LogicalName!, FieldTypes.UniqueIdentifier);
            // Add attribute to FetchXml.
            var field = fieldsAttributes.First(x => x.FieldType == FieldTypes.UniqueIdentifier);
            var xAttribute = new XElement("attribute");
            xAttribute.SetAttributeValue("name", field.LogicalName);
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
