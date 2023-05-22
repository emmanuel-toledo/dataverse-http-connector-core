namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// This is a custom exception class for "Unauthorized" exception.
    /// </summary>
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }

        public UnauthorizedException(object key) : base($"The connection to '{key}' is not authorized.") { }
    }
}
