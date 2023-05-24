using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Extensions.InternalInjection;

namespace  Dynamics.Crm.Http.Connector.Core.Facades.Requests
{
    /// <summary>
    /// This interface defines the method to call each request to Dynamics.
    /// </summary>
    public interface IDynamicsRequest
    {
        /// <summary>
        /// Method to send a request to SharePoint
        /// </summary>
        /// <param name="request">Http request message configuration.</param>
        /// <returns>Http response message.</returns>
        Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request);
    }

    /// <summary>
    /// This class implements the method to call each request to Dynamics.
    /// </summary>
    public class DynamicsRequest : IDynamicsRequest
    {
        /// <summary>
        /// Private Dynamics builder service to get global configuration.
        /// </summary>
        private readonly IDynamicsBuilder _builder;

        /// <summary>
        /// Private http client factory service to build http client.
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Private Dynamics authenticator to get access token according Dynamics builder configuration.
        /// </summary>
        private readonly IDynamicsAuthenticator _authenticator;

        /// <summary>
        /// Create new instance of "DynamicsRequest" class to consume Dynamics API.
        /// </summary>
        /// <param name="builder">IDynamicsBuilder service.</param>
        /// <param name="clientFactory">IHttpClientFactory service.</param>
        /// <param name="authenticator">IDynamicsAuthenticator service.</param>
        public DynamicsRequest(IDynamicsBuilder builder, IHttpClientFactory clientFactory, IDynamicsAuthenticator authenticator)
        {
            _builder = builder;
            _clientFactory = clientFactory;
            _authenticator = authenticator;
        }

        /// <summary>
        /// Function to request information to Dynamics CRM using REST API.
        /// </summary>
        /// <param name="request">Request message to api execution.</param>
        /// <returns>Response message from api execution.</returns>
        public async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request)
        {
            try
            {
                // Configure client connector to Dynamics CRM.
                var client = _clientFactory.ConfigureDynamicsEnvironmentClient(_authenticator, _builder);
                // Execute Dynamics CRM request and validate exception.
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode && _builder.ThrowExceptions)
                    return null;
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
