namespace Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse
{
    /// <summary>
    /// This exception indicates that a client is prohibited from accessing the environment.
    /// <para>
    /// Expect this for the following types of errors:
    /// </para>
    /// <para>
    /// - AccessDenied
    /// - AttributePermissionReadIsMissing
    /// - AttributePermissionUpdateIsMissingDuringUpdate
    /// - AttributePrivilegeCreateIsMissing
    /// - CannotActOnBehalfOfAnotherUser
    /// - CannotAddOrActonBehalfAnotherUserPrivilege
    /// - SecurityError
    /// - InvalidAccessRights
    /// - PrincipalPrivilegeDenied
    /// - PrivilegeCreateIsDisabledForOrganization
    /// - PrivilegeDenied
    /// - unManagedinvalidprincipal
    /// - unManagedinvalidprivilegeedepth
    /// </para>
    /// <para>
    /// Status code to throw exception is 403.
    /// </para>
    /// </summary>
    [Serializable]
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Create a new Forbidden exception.
        /// </summary>
        public ForbiddenException() : base() { }

        /// <summary>
        /// Create a new Forbidden exception with custom message.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        public ForbiddenException(string message) : base(message) { }

        /// <summary>
        /// Create a new Forbidden exception with custom message and inner exception.
        /// </summary>
        /// <param name="message">Error custom message.</param>
        /// <param name="innerException">Details exception instance.</param>
        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Create a new Forbidden exception with default message.
        /// </summary>
        /// <param name="key">Client key identifier.</param>
        public ForbiddenException(object key) : base($"the client ('{key}') is prohibited from accessing the environment.") { }
    }
}
