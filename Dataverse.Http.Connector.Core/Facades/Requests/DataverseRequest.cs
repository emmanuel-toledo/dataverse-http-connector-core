using Dataverse.Http.Connector.Core.Extensions.Utilities;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Business.Authentication;
using Dataverse.Http.Connector.Core.Extensions.DependencyInjections.Configurations;

namespace Dataverse.Http.Connector.Core.Facades.Requests
{
    /// <summary>
    /// This interface defines the method to call each request to Dataverse.
    /// </summary>
    internal interface IDataverseRequest
    {
        /// <summary>
        /// Method to send a request to SharePoint
        /// </summary>
        /// <param name="request">Http request message configuration.</param>
        /// <returns>Http response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool isGeneric);
    }

    /// <summary>
    /// This class implements the method to call each request to Dataverse.
    /// </summary>
    internal class DataverseRequest : IDataverseRequest
    {
        /// <summary>
        /// Private Dataverse builder service to get global configuration.
        /// </summary>
        private readonly IDataverseBuilder _builder;

        /// <summary>
        /// Private http client factory service to build http client.
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Private Dataverse authenticator to get access token according Dataverse builder configuration.
        /// </summary>
        private readonly IDataverseAuthenticator _authenticator;

        /// <summary>
        /// Create new instance of "DataverseRequest" class to consume Dataverse API.
        /// </summary>
        /// <param name="builder">IDataverseBuilder service.</param>
        /// <param name="clientFactory">IHttpClientFactory service.</param>
        /// <param name="authenticator">IDataverseAuthenticator service.</param>
        public DataverseRequest(IDataverseBuilder builder, IHttpClientFactory clientFactory, IDataverseAuthenticator authenticator)
        {
            _builder = builder;
            _clientFactory = clientFactory;
            _authenticator = authenticator;
        }

        /// <summary>
        /// Function to request information to Dataverse  using REST API.
        /// </summary>
        /// <param name="request">Request message to api execution.</param>
        /// <returns>Response message from api execution.</returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool isGeneric = false)
        {
            try
            {
                // Configure client connector to Dataverse .
                var client = _clientFactory.ConfigureDataverseEnvironmentClient(_authenticator, _builder);
                // Execute Dataverse  request and validate exception.
                var response = await client.SendAsync(request);
                return response.EvaluateDataverseResponse(isGeneric, _builder.ThrowExceptions);
            }
            catch
            {
                throw;
            }
        }
    }
}
