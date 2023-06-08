namespace Dynamics.Crm.Http.Connector.Core.Domains.Enums
{
    /// <summary>
    /// This enum works to define the types of aggregates that we can use in a FetchXml query.
    /// </summary>>
    public enum AggregateTypes
    {
        AVG,
        COUNT,
        COUNTCOLUMN,
        MAX,
        MIN,
        SUM
    }

    /// <summary>
    /// This class defines the aggregates that can be used in a FetchXml query.
    /// </summary>
    public static class Aggregates
    {
        /// <summary>
        /// Function to parse the aggregate type to a string value of a FetchXml query.
        /// </summary>
        /// <param name="aggregateType">Selected aggregate type.</param>
        /// <returns>Aggregate type in string format</returns>
        /// <exception cref="NullReferenceException">Any aggregate was not selected.</exception>
        internal static string Parse(AggregateTypes aggregateType)
        {
            return aggregateType switch
            {
                AggregateTypes.AVG => "avg",
                AggregateTypes.COUNT => "count",
                AggregateTypes.COUNTCOLUMN => "countcolumn",
                AggregateTypes.MAX => "max",
                AggregateTypes.MIN => "min",
                AggregateTypes.SUM => "sum",
                _ => throw new NullReferenceException("Any aggregate type was not selected."),
            };
        }
    }
}
