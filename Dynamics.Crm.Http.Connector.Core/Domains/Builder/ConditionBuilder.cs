using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Utilities;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Builder
{
    /// <summary>
    /// This class contains properties according the attributes that a FetchXml condition needs.
    /// </summary>
    public class ConditionBuilder
    {
        /// <summary>
        /// Get and set property to evaluate in a query in the "condition" tag.
        /// </summary>
        private string? Property { get; set; } = string.Empty;

        /// <summary>
        /// Get and set condition to use in a query in the "condition" tag.
        /// </summary>
        private ConditionTypes Condition { get; set; }

        /// <summary>
        /// Get and set value to check in a query in the "condition" tag.
        /// </summary>
        private string? Value { get; set; } = string.Empty;

        /// <summary>
        /// Creates a new instance of "ConditionBuilder" to be used as a new "condition" tag in a FetchXml query.
        /// </summary>
        /// <param name="propery">Property name to evaluate.</param>
        /// <param name="condition">Condition to use.</param>
        /// <param name="value">Value to check.</param>
        public ConditionBuilder(string? propery, ConditionTypes condition, string? value)
        {
            Property = propery;
            Condition = condition;
            Value = value;
        }

        /// <summary>
        /// This function creates a new "condition" tag for a FetchXml query to be used to consume Dynamics service.
        /// </summary>
        /// <returns>Condition tag in string format.</returns>
        internal string Build()
        {
            Check.NotNullOrEmpty(Property, nameof(Property));
            Check.NotNull(Condition, nameof(Condition));
            // TODO: Check conditions types to generate "condition" tag to query.
            switch (Condition)
            {
                case ConditionTypes.In:
                case ConditionTypes.NotIn:
                    return @$"<condition attribute='{Property}' operator='{Parse.ParseCondition(Condition)}'>
                            {Value}
                           </condition>";
                default:
                    return $"<condition attribute='{Property}' operator='{Parse.ParseCondition(Condition)}' value='{Value}' />";
            }
        }
    }
}
