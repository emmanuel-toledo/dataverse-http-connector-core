namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// This class works to define a type of exception where an entity (class) was not configured to use the library.
    /// </summary>
    public class NotDefinitionEntityException : Exception
    {
        /// <summary>
        /// Throw a new default exception.
        /// </summary>
        public NotDefinitionEntityException() : base() { }

        /// <summary>
        /// Throw a new exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public NotDefinitionEntityException(string message) : base(message) { }

        /// <summary>
        /// Throw a new exception with custom message and details exception. 
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public NotDefinitionEntityException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Throw a new exception with default message.
        /// </summary>
        /// <param name="name">Configured entity name.</param>
        /// <param name="key">Configured entity type.</param>
        public NotDefinitionEntityException(string name, object key) : base($"Entity with name '{name}' and type { key } was not configured in the scheme.") { }
    }
}
