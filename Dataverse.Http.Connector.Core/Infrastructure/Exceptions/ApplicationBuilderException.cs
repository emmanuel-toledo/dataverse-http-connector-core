namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// This class is a custom exception for internal "Application Builder" exception.
    /// </summary>
    [Serializable]
    public class ApplicationBuilderException : Exception
    {
        /// <summary>
        /// Create a new application builder exception.
        /// </summary>
        public ApplicationBuilderException() : base() { }

        /// <summary>
        /// Create a new application builder exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public ApplicationBuilderException(string message) : base(message) { }

        /// <summary>
        /// Create a new application builder exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public ApplicationBuilderException(string message, Exception innerException) : base(message, innerException) { }
    }
}
