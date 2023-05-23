using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Extensions.InternalInjection;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;

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
        private readonly IDynamicsBuilder _builder;

        private readonly IHttpClientFactory _clientFactory;

        private readonly IDynamicsAuthenticator _authenticator;

        public DynamicsRequest(IDynamicsBuilder builder, IHttpClientFactory clientFactory, IDynamicsAuthenticator authenticator)
        {
            _builder = builder;
            _clientFactory = clientFactory;
            _authenticator = authenticator;
        }

        public async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request)
        {
            try
            {
                // Configure client connector to Dynamics CRM.
                var client = _clientFactory.CreateClient("DynamicsClient").ConfigureClientEnvironment(_authenticator, _builder);
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
