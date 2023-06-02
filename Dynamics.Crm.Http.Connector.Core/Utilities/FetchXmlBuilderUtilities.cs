using System.Xml.Linq;
using Dynamics.Crm.Http.Connector.Core.Domains.Xml;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using System.Collections;

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
            xCondition.SetAttributeValue("operator", Parse.ParseCondition(model.ConditionType));
            // Evaluate each kind of condition type.
            switch (model.ConditionType)
            {
                // TODO: Por cada valor de "value" hacer una etiqueta <value>{ valor }</value> en la condición.
                case ConditionTypes.In:
                case ConditionTypes.NotIn:
                    foreach(var value in (model.Value as IEnumerable)!)
                    {
                        var xValue = new XElement("value", value);
                        xCondition.Add(xValue);
                    }
                    break;
                case ConditionTypes.Equal:
                case ConditionTypes.NotEqual:
                case ConditionTypes.Null:
                case ConditionTypes.NotNull:
                case ConditionTypes.BeginsWith:
                case ConditionTypes.DoesNotBeginWith:
                case ConditionTypes.EndsWith:
                case ConditionTypes.DoesNotEndsWith:
                case ConditionTypes.Like:
                case ConditionTypes.NotLike:
                    xCondition.SetAttributeValue("value", model.Value);
                    break;
                default:
                    throw new ArgumentNullException($"The condition type with name '{nameof(model)}' can not be defined.");
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
            xFilter.SetAttributeValue("type", Parse.ParseFilter(model.FilterType));
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
        /// <exception cref="NullReferenceException">The TEntity model does not use Entity Attributes annotations.</exception>
        public static string CreateEntityFetchXmlQuery<TEntity>(FetchXml model) where TEntity : class, new()
        {
            // Create a new instance of TEntity object.
            var entity = Instance<TEntity>.TEntityInstance().GetType();
            // Get entity annotation attributes.
            var entityAttributes = Annotations.GetEntityAttributes(entity) ?? throw new NullReferenceException($"The entity attributes definitions in class {entity.Name} is null.");
            // Get entity fields annotation attributes.
            var fieldsAttributes = Annotations.GetFieldsAttributes(entity);
            // Create new XML for query document.
            var xDocument = new XDocument();
            // Create main element.
            var xFetch = new XElement("fetch");
            if (model.Top > 0)
                xFetch.SetAttributeValue("top", model.Top);
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
