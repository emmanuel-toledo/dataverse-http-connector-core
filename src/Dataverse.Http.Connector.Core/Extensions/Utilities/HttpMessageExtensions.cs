using Dataverse.Http.Connector.Core.Domains.Enums;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse;
using NotImplementedException = Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse.NotImplementedException;

namespace Dataverse.Http.Connector.Core.Extensions.Utilities
{
    /// <summary>
    /// This class works to define the required methods to use for HTTP messages.
    /// </summary>
    internal static class HttpMessageExtensions
    {
        /// <summary>
        /// Function that evaluates if the response was failed or not.
        /// </summary>
        /// <param name="response">Http response message instance.</param>
        /// <param name="isGeneric">It is a generic request.</param>
        /// <param name="throwExceptions">Throw exception in failed response.</param>
        /// <returns>Instance of http response message.</returns>
        public static HttpResponseMessage EvaluateDataverseResponse(this HttpResponseMessage response, bool isGeneric, bool throwExceptions)
        {
            // Validate if request was successful.
            if (response.IsSuccessStatusCode)
                return response;
            // If request was failed and throw exceptions flag is negative.
            if (!throwExceptions && isGeneric == true)
                return response;
            // Evaluate base method.
            return response.ThrowDataverseException();
        }

        /// <summary>
        /// Function to evaluate each failed response code and thow exception.
        /// </summary>
        /// <param name="response">Http response message instance.</param>
        /// <returns>Instance of http response message.</returns>
        public static HttpResponseMessage ThrowDataverseException(this HttpResponseMessage response)
        {
            // Evaluate status and get content.
            var status = (int)response.StatusCode;
            var content = response.Content.ReadAsStringAsync().Result;
            var exception = new Exception();
            switch (status)
            {
                case (int)ExceptionsTypes.NotModified:
                    exception = new NotModifiedException($"The entity record could not be modified.\nError code {(int)ExceptionsTypes.NotModified}.");
                    break;
                case (int)ExceptionsTypes.Forbidden:
                    exception = new ForbiddenException($"Cannot access to Dataverse environment with current credentials.\nError code {(int)ExceptionsTypes.Forbidden}.");
                    break;
                case (int)ExceptionsTypes.Unauthorized:
                    exception = new UnauthorizedException($"The current credentials has no access to this resource of Dataverse.\nError code {(int)ExceptionsTypes.Unauthorized}.");
                    break;
                case (int)ExceptionsTypes.PayloadTooLarge:
                    exception = new PayloadTooLargeException($"The request contains a payload that is over the limit available.\nError code {(int)ExceptionsTypes.PayloadTooLarge}.");
                    break;
                case (int)ExceptionsTypes.BadRequest:
                    exception = new BadRequestException($"The request does not contains the correct format.\nError code {(int)ExceptionsTypes.BadRequest}.");
                    break;
                case (int)ExceptionsTypes.NotFound:
                    exception = new NotFoundException($"The required resource could not be found it.\nError code {(int)ExceptionsTypes.NotFound}.");
                    break;
                case (int)ExceptionsTypes.MethodNotAllowed:
                    exception = new MethodNotAllowedException($"The request method is not supported for this operation.\nError code {(int)ExceptionsTypes.MethodNotAllowed}.");
                    break;
                case (int)ExceptionsTypes.PreconditionFailed:
                    exception = new PreconditionFailedException($"The request does not match with concurrency version or is duplicating a record.\nError code {(int)ExceptionsTypes.PreconditionFailed}.");
                    break;
                case (int)ExceptionsTypes.TooManyRequests:
                    exception = new TooManyRequestsException($"Too many requests have been executed in Dataverse.\nError code {(int)ExceptionsTypes.TooManyRequests}.");
                    break;
                case (int)ExceptionsTypes.NotImplemented:
                    exception = new NotImplementedException($"The request try to execute a not implemented method.\nError code {(int)ExceptionsTypes.NotImplemented}.");
                    break;
                case (int)ExceptionsTypes.ServiceUnavailable:
                    exception = new ServiceUnavailableException($"The service is not available.\nError code {(int)ExceptionsTypes.ServiceUnavailable}.");
                    break;
                default:
                    break;
            }
            if (content != null)
                exception.Data.Add("Content", content);
            throw exception;
        }
    }
}
