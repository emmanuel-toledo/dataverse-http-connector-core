namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// This class is a custom exception for "Internal server error" exception.
    /// </summary>
    [Serializable]
    public class InternalServerException : Exception
    {
        public InternalServerException() : base() { }

        public InternalServerException(string message) : base(message) { }

        public InternalServerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
