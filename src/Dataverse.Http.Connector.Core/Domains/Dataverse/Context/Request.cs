using Dataverse.Http.Connector.Core.Domains.Enums;

namespace Dataverse.Http.Connector.Core.Domains.Dataverse.Context
{
    /// <summary>
    /// This class contains properties to call a new http request.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// New class empty instance for a request.
        /// </summary>
        public Request() {}

        /// <summary>
        /// Get and set FetchXml string for logger.
        /// </summary>
        public string? FetchXml { get; set; } = string.Empty;

        /// <summary>
        /// Get and set request endpoint url.
        /// </summary>
        public string? EndPoint { get; set; } = string.Empty;

        /// <summary>
        /// Get and set base method to check which call must be requested.
        /// </summary>
        public BaseMethodTypes Method { get; set; }

        /// <summary>
        /// Get and set dictionary params collection.
        /// </summary>
		public Dictionary<string, string> Params { get; set; } = new();

        /// <summary>
        /// Function to add a new custom param to dictionary params collection.
        /// </summary>
        /// <param name="key">Param name.</param>
        /// <param name="value">Param value.</param>
        public void AddParam(string key, string value)
            => Params.Add(key, value);

        /// <summary>
        /// Function to add a "fetchXml" param to dictionary params collection.
        /// </summary>
        /// <param name="query">FetchXml query.</param>
        public void AddQueryParam(string query)
            => Params.Add("fetchXml", query);
    }
}
