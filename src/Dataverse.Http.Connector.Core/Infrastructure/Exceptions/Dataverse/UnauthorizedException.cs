namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a connection is not authorized.
    /// <para>
    /// Expect this for the following types of errors:
    /// </para>
    /// <para>
    /// - BadAuthTicket
    /// - ExpiredAuthTicket
    /// - InsufficientAuthTicket
    /// - InvalidAuthTicket
    /// - InvalidUserAuth
    /// - MissingAuthenticationToken
    /// - MissingAuthenticationTokenOrganizationName
    /// - RequestIsNotAuthenticated
    /// - TamperedAuthTicket
    /// - UnauthorizedAccess
    /// - UnManagedInvalidSecurityPrincipal
    /// </para>
    /// <para>
    /// Status code to throw exception is 401.
    /// </para>
    /// </summary>
    [Serializable]
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Create a new unauthorized exception.
        /// </summary>
        public UnauthorizedException() : base() { }

        /// <summary>
        /// Create a new unauthorized exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public UnauthorizedException(string message) : base(message) { }

        /// <summary>
        /// Create a new unauthorized exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Create a new unauthorized exception with default message.
        /// </summary>
        /// <param name="key">Object key identifier.</param>
        public UnauthorizedException(object key) : base($"The connection to '{key}' is not authorized.") { }
    }
}
