namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a entity hasn't been modified.
    /// <para>
    /// Status code to throw exception is 304.
    /// </para>
    /// </summary>
    [Serializable]
    internal class NotModifiedException : Exception
    {
        /// <summary>
        /// Create a new not modified exception.
        /// </summary>
        public NotModifiedException() : base() { }

        /// <summary>
        /// Create a new not modified exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public NotModifiedException(string message) : base(message) { }

        /// <summary>
        /// Create a new not modified exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public NotModifiedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
