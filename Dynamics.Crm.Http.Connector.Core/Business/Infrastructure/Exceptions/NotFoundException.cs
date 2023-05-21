namespace  Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Exceptions
{
    /// <summary>
    /// This class is a custom exception for "Not found" exception.
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public NotFoundException(string name, object key) : base($"Resource '{name}' ('{key}' was not found.)") { }
    }
}
