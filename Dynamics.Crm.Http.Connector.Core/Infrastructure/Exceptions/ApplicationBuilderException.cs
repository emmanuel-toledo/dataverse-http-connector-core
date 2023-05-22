namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// This class is a custom exception for internal "Application Builder" exception.
    /// </summary>
    [Serializable]
    public class ApplicationBuilderException : Exception
    {
        public ApplicationBuilderException() : base() { }

        public ApplicationBuilderException(string message) : base(message) { }

        public ApplicationBuilderException(string message, Exception innerException) : base(message, innerException) { }
    }
}
