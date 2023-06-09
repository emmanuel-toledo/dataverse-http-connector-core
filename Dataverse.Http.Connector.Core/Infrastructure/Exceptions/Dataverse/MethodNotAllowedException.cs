namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a method type is not supported.
    /// <para>
    /// Expect this for the following types of errors:
    /// </para>
    /// <para>
    /// - CannotDeleteDueToAssociation
    /// - InvalidOperation
    /// - NotSupported
    /// </para>
    /// <para>
    /// Status code to throw exception is 405.
    /// </para>
    /// </summary>
    [Serializable]
    public class MethodNotAllowedException : Exception
    {
        /// <summary>
        /// Create a new method not allowed exception.
        /// </summary>
        public MethodNotAllowedException() : base() { }

        /// <summary>
        /// Create a new method not allowed exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public MethodNotAllowedException(string message) : base(message) { }

        /// <summary>
        /// Create a new method not allowed exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public MethodNotAllowedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
