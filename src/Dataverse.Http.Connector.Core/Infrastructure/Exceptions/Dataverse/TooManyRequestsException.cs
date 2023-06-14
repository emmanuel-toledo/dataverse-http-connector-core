namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that API limits are exceeded.
    /// <para>
    /// Status code to throw exception is 429.
    /// </para>
    /// </summary>
    [Serializable]
    public class TooManyRequestsException : Exception
    {
        /// <summary>
        /// Create a new too many requests exception.
        /// </summary>
        public TooManyRequestsException() : base() { }

        /// <summary>
        /// Create a new too many requests exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public TooManyRequestsException(string message) : base(message) { }

        /// <summary>
        /// Create a new too many requests exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public TooManyRequestsException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Create a new too many requests exception with default message.
        /// </summary>
        /// <param name="key">Resource's key.</param>
        public TooManyRequestsException(object key) : base($"Too many requests have been executed in the resource ('{key}').") { }
    }
}
