namespace Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Exceptions
{
    /// <summary>
    /// This class is a custom exception for "Bad request" exception.
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
