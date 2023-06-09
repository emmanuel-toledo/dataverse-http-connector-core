namespace Dataverse.Http.Connector.Core.Domains.Enums
{
    /// <summary>
    /// This enum contains the possible exceptions for Dataverse.
    /// </summary>
    internal enum ExceptionsTypes
    {
        NotModified = 304,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        TooManyRequests = 429,
        NotImplemented = 501,
        ServiceUnavailable = 503
    }
}
