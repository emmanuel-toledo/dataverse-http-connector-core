namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a resource was not found in the schema.
    /// <para>
    /// Status code to throw exception is 404.
    /// </para>
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Create a new not found exception.
        /// </summary>
        public NotFoundException() : base() { }

        /// <summary>
        /// Create a new not found exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Create a new not found exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Create a new not found exception with default message.
        /// </summary>
        /// <param name="name">Resource's name.</param>
        /// <param name="key">Resource's key.</param>
        public NotFoundException(string name, object key) : base($"Resource '{name}' ('{key}' was not found.)") { }
    }
}
