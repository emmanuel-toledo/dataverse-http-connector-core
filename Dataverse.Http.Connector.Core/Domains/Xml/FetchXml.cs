using Dataverse.Http.Connector.Core.Domains.Enums;

namespace Dataverse.Http.Connector.Core.Domains.Xml
{
    /// <summary>
    /// This class contains the properties used to create a "fetchxml" query.
    /// </summary>
    public class FetchXml
    {
        /// <summary>
        /// Create a new instance of FetchXml object.
        /// </summary>
        public FetchXml() {}

        /// <summary>
        /// Get an set the top records to retrive in a FetchXml query.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Get an set the page in a FetchXml query.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Get an set the page size in a FetchXml query.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Get an set the flag to distinc records in a FetchXml query.
        /// </summary>
        public bool Distinct { get; set; }

        /// <summary>
        /// Get an set the flag to aggregate records in a FetchXml query.
        /// </summary>
        public bool Aggregate { get; set; }

        /// <summary>
        /// Get an set the list of filters to be used in a FetchXml query.
        /// </summary>
        public ICollection<Filter> Filters { get; set; } = new HashSet<Filter>();

        /// <summary>
        /// Function to add a new filter in the filters collection.
        /// </summary>
        /// <param name="type">Filter type to new Filter element.</param>
        public void AddFilter(FilterTypes type, ICollection<Condition> conditions)
            => Filters.Add(new Filter(type, conditions));
    }
}
