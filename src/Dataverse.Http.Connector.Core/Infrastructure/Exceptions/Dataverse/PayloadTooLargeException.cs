namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a request length is too large.
    /// <para>
    /// Status code to throw exception is 413.
    /// </para>
    /// </summary>
    [Serializable]
    public class PayloadTooLargeException : Exception
    {
        /// <summary>
        /// Create a new payload too large exception.
        /// </summary>
        public PayloadTooLargeException() : base() { }

        /// <summary>
        /// Create a new payload too large exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public PayloadTooLargeException(string message) : base(message) { }

        /// <summary>
        /// Create a new payload too large exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public PayloadTooLargeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
