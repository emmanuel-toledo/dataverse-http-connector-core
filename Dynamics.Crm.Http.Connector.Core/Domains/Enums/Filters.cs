namespace Dynamics.Crm.Http.Connector.Core.Domains.Enums
{
    /// <summary>
    /// This enum works to define the Filters Type that you can use as a FetchXml query.
    /// </summary>
    public enum FilterTypes
    {
        And,
        Or
    }

    /// <summary>
    /// This class defines the filter types that can be used in a FetchXml query.
    /// </summary>
    internal static class Filters
    {
        /// <summary>
        /// Function to parse a Filter Type in a FetchXml filter string.
        /// </summary>
        /// <param name="filterType">Enum filter types.</param>
        /// <returns>FetchXml filter string.</returns>
        /// <exception cref="ArgumentNullException">Filter type was not recognized.</exception>
        internal static string Parse(FilterTypes filterType)
        {
            return filterType switch
            {
                FilterTypes.Or => "or",
                FilterTypes.And => "and",
                _ => throw new NullReferenceException("Any filter type was not selected."),
            };
        }
    }
}
