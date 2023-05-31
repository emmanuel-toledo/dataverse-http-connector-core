using Dynamics.Crm.Http.Connector.Core.Domains.Enums;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Xml
{
    /// <summary>
    /// This class contains the properties used to add a "filter" tag for "fetchxml" query.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Initialize a new instance of Filter object to be used in "fetchxml" query.
        /// </summary>
        /// <param name="type">Filter type (and, or).</param>
        public Filter(FilterTypes type)
            => FilterType = type;

        /// <summary>
        /// Initialize a new instance of Filter object to be used in "fetchxml" query.
        /// </summary>
        /// <param name="conditions">Conditions collection.</param>
        public Filter(ICollection<Condition> conditions)
            => Conditions = conditions;

        /// <summary>
        /// Initialize a new instance of Filter object to be used in "fetchxml" query.
        /// </summary>
        /// <param name="type">Filter type (and, or).</param>
        /// <param name="conditions">Conditions collection.</param>
        public Filter(FilterTypes type, ICollection<Condition> conditions)
        {
            FilterType = type;
            Conditions = conditions;
        }

        /// <summary>
        /// Get and set a Filter type for specific Filter.
        /// </summary>
        public FilterTypes FilterType { get; set; }

        /// <summary>
        /// List of new entities conditions to evaluate inside a filter.
        /// </summary>
        public ICollection<Condition> Conditions { get; set; } = new HashSet<Condition>();
    }
}
