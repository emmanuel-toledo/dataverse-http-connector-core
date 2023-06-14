namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a method is not implemented in the schema.
    /// <para>
    /// Status code to throw exception is 501.
    /// </para>
    /// </summary>
    [Serializable]
    public class NotImplementedException : Exception
    {
        /// <summary>
        /// Create a new not implemented exception.
        /// </summary>
        public NotImplementedException() : base() { }

        /// <summary>
        /// Create a new not implemented exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public NotImplementedException(string message) : base(message) { }

        /// <summary>
        /// Create a new not implemented exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public NotImplementedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
