using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Domains.Xml;
using System.Xml.Linq;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class FetchXmlBuilderUtilities
    {
        /// <summary>
        /// This function creates a new "condition" tag for a FetchXml query to be used to consume Dynamics service.
        /// </summary>
        /// <returns>Condition tag in string format.</returns>
        public static string CreateConditionQuery(Condition condition)
        {
            Check.NotNullOrEmpty(condition.Property, nameof(condition.Property));
            Check.NotNull(condition.ConditionType, nameof(condition.ConditionType));
            // TODO: Check conditions types to generate "condition" tag to query.
            switch (condition.ConditionType)
            {
                case ConditionTypes.In:
                case ConditionTypes.NotIn:
                    return @$"<condition attribute='{condition.Property}' operator='{Parse.ParseCondition(condition.ConditionType)}'>
                            {condition.Value}
                           </condition>";
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
                    return $"<condition attribute='{condition.Property}' operator='{Parse.ParseCondition(condition.ConditionType)}' value='{condition.Value}' />";
                default:
                    throw new ArgumentNullException($"The condition type with name '{nameof(condition)}' can not be defined.");
            }
        }

        /// <summary>
        /// This function creates a new "filter" tag for a FetchXml query to be used to consume Dynamics service.
        /// <para>
        /// Already includes specified "condition" tags.
        /// </para>
        /// </summary>
        /// <returns>Filter tag in string format.</returns>
        public static string CreateFilterQuery(Filter filter)
        {
            string query = $"<filter type='{Parse.ParseFilter(filter.FilterType)}'>";
            foreach (var condition in filter.Conditions)
                query += CreateConditionQuery(condition);
            query += "</filter>";
            return query;
        }

        public static string CreateEntityFetchXmlQuery<TEntity>(FetchXml fetchXml) where TEntity : class, new()
        {
            // Create a new instance of TEntity object.
            var entity = Instance<TEntity>.TEntityInstance().GetType();
            // Get entity annotation attributes.
            var entityAttributes = Annotations.GetEntityAttributes(entity) ?? throw new NullReferenceException($"The entity attributes definitions in class {entity.Name} is null.");
            // Get entity fields annotation attributes.
            var fieldsAttributes = Annotations.GetFieldsAttributes(entity);
            // Create FetchXml query.


            // TODO: Create fetchxml query using XDocument class to set alias to each attribute using SchemaNames.
            //var query = new XDocument();
            
            string query = @$"<fetch { (fetchXml.Top > 0 ? $"top='{fetchXml.Top}'" : "") } {(fetchXml.Distinct ? $"distinct='{fetchXml.Distinct.ToString().ToLower()}'" : "")}>
            <entity name='{ entityAttributes.SchemaName }'>";
            foreach (var field in fieldsAttributes)
                query += $"  <attribute name='{ field.LogicalName }' /> \n";
            foreach (var filter in fetchXml.Filters)
                query += CreateFilterQuery(filter);
            query += @"</entity></fetch>";
            // Set format to query and retrive Fetch Xml.
            var xml = XDocument.Parse(query);
            return xml.ToString();
        }
    }
}
