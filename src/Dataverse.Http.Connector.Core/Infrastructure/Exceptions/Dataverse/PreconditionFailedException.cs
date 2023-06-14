namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that one preconditions failed. 
    /// <para>
    /// Expect this for the following types of errors:
    /// </para>
    /// <para>
    /// - ConcurrencyVersionMismatch
    /// - DuplicateRecord
    /// </para>
    /// <para>
    /// Status code to throw exception is 412.
    /// </para>
    /// </summary>
    [Serializable]
    public class PreconditionFailedException : Exception
    {
        /// <summary>
        /// Create a new precondition failed exception.
        /// </summary>
        public PreconditionFailedException() : base() { }

        /// <summary>
        /// Create a new precondition failed exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public PreconditionFailedException(string message) : base(message) { }

        /// <summary>
        /// Create a new precondition failed exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public PreconditionFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
