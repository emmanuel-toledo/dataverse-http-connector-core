using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Dataverse.Http.Connector.Core.Domains.Enums;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Business.Authentication;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Context;

namespace Dataverse.Http.Connector.Core.Extensions.DependencyInjections.Configurations
{
    /// <summary>
    /// Internal extension class to configure HttpClient configurations.
    /// </summary>
    internal static class HttpClientExtensions
    {
        /// <summary>
        /// Configure client authentication and base url configuration to consume Dataverse data.
        /// </summary>
        /// <param name="clientFactory">Dataverse http client.</param>
        /// <param name="authenticator">Dataverse authentication service instance.</param>
        /// <param name="builder">Dataverse builder service instance.</param>
        internal static HttpClient ConfigureDataverseEnvironmentClient(this IHttpClientFactory clientFactory, IDataverseAuthenticator authenticator, IDataverseBuilder builder)
        {
            try
            {
                // Build "DataverseClient" http client.
                var client = clientFactory.CreateClient("DataverseClient");
                // Validate if exists a previous authentication token and is valid to use.
                if (!builder.IsValidAuthentication(builder.Connection!.Resource!))
                {
                    // Get authentication token result using "IDataverseAuthenticator" service.
                    var authentication = authenticator.AuthenticateAsync().Result;

                    // Add authorization if success authentication.
                    if (authentication != null && !string.IsNullOrEmpty(authentication.AccessToken))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authentication.TokenType, authentication.AccessToken);
                }
                else
                {
                    // Set Authorization token using cached Authentication Result.
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(builder.Authentication!.TokenType, builder.Authentication!.AccessToken);
                }

                // Set authentication token according to DataverseBuilder information.
                client.BaseAddress = new Uri($"{builder.Connection!.Resource!}/api/data/v{(builder.Connection!.Version == 0.0 ? "9.2" : builder.Connection!.Version)}/");
                return client;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the configuration of the service client for Dataverse", ex);
            }
        }

        /// <summary>
        /// Initialize default BaseAddress configuration for a client to consume Dataverse API.
        /// </summary>
        /// <param name="client">Dataverse http client.</param>
        internal static void ConfigureDefaultClientConfiguration(this HttpClient client)
        {
            // Configure default client configuration.
            client.Timeout = new TimeSpan(0, 2, 0);
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=\"*\"");
            client.DefaultRequestHeaders.Add("OData-Version", "4.0");
            client.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        }

        /// <summary>
        /// Extension method to conver "Request" object instance to "HttpRequestMessage".
        /// </summary>
        /// <param name="request">Request configuration object.</param>
        /// <returns>Instance of http request message.</returns>
        internal static HttpRequestMessage ConvertToHttpRequest(this Request request)
            => new(BaseMethods.ParseToHttpMethod(request.Method), QueryHelpers.AddQueryString(request.EndPoint, request.Params));
    }
}
