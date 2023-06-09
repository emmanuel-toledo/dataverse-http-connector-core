using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// This class works to define a type of exception where an entity (class) was not configured to use the library.
    /// </summary>
    [Serializable]
    public class EntityDefinitionException : Exception
    {
        /// <summary>
        /// Create a new entity deffinition exception.
        /// </summary>
        public EntityDefinitionException() : base() { }

        /// <summary>
        /// Create a new entity deffinition exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public EntityDefinitionException(string message) : base(message) { }

        /// <summary>
        /// Create a new entity deffinition exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public EntityDefinitionException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Create a new entity deffinition exception with default message.
        /// </summary>
        /// <param name="name">Configured entity name.</param>
        /// <param name="key">Configured entity type.</param>
        public EntityDefinitionException(string name, object key) : base($"Entity with name '{name}' and type { key } was not configured in the scheme.") { }

        /// <summary>
        /// Create a new entity deffinition exception with default message.
        /// </summary>
        /// <param name="name">Configured entity name.</param>
        /// <param name="fieldType">Missed field type configured.</param>
        public EntityDefinitionException(string name, FieldTypes fieldType) : base($"Entity with name '{ name }' does not contains a Field Attribute of type '{ fieldType }' in any property.") { }
    }
}
