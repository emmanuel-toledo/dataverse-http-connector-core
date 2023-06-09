namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that the web API service isn't available.
    /// <para>
    /// Status code to throw exception is 503.
    /// </para>
    /// </summary>
    [Serializable]
    public class ServiceUnavailableException : Exception
    {
        /// <summary>
        /// Create a new service unavailable exception.
        /// </summary>
        public ServiceUnavailableException() : base() { }

        /// <summary>
        /// Create a new service unavailable exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public ServiceUnavailableException(string message) : base(message) { }

        /// <summary>
        /// Create a new service unavailable exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }
    }
}
