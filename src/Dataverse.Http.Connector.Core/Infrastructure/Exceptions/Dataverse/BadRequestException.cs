namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a client request was not created correctly.
    /// <para>
    /// Status code to throw exception is 400.
    /// </para>
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Create a new bad request exception.
        /// </summary>
        public BadRequestException() : base() { }

        /// <summary>
        /// Create a new bad request exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public BadRequestException(string message) : base(message) { }

        /// <summary>
        /// Create a new bad request exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
