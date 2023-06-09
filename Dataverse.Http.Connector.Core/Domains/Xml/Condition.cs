using Dataverse.Http.Connector.Core.Domains.Enums;

namespace Dataverse.Http.Connector.Core.Domains.Xml
{
    /// <summary>
    /// This class contains the properties used to add "conditions" in a "filter" tag for "fetchxml" query.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Initialize a new instance of Condition object to be used in "fetchxml" query.
        /// </summary>
        /// <param name="property">Property of entity to evaluate.</param>
        /// <param name="conditionTypes">Condition type to use.</param>
        /// <param name="value">Value to evaluate.</param>
        public Condition(string? property, ConditionTypes conditionTypes, object value)
        {
            Property = property;
            ConditionType = conditionTypes;
            Value = value;
        }

        /// <summary>
        /// Get and set property to evaluate in a query in the "condition" tag.
        /// </summary>
        public string? Property { get; set; } = string.Empty;

        /// <summary>
        /// Get and set condition to use in a query in the "condition" tag.
        /// </summary>
        public ConditionTypes ConditionType { get; set; }

        /// <summary>
        /// Get and set value to check in a query in the "condition" tag.
        /// </summary>
        public object Value { get; set; }
    }
}
