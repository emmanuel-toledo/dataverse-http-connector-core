﻿using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Business.Authentication;

namespace Dynamics.Crm.Http.Connector.Core.Extensions.InternalInjection
{
    /// <summary>
    /// Internal extension class to configure HttpClient configurations.
    /// </summary>
    internal static class HttpClientExtensions
    {
        /// <summary>
        /// Configure client authentication and base url configuration to consume Dynamics data.
        /// </summary>
        /// <param name="client">Dynamics http client.</param>
        /// <param name="authenticator">Dynamics authentication service instance.</param>
        /// <param name="builder">Dynamics builder service instance.</param>
        internal static HttpClient ConfigureClientEnvironment(this HttpClient client, IDynamicsAuthenticator authenticator, IDynamicsBuilder builder)
        {
            try
            {
                // Validate if exists a previous authentication token and is valid to use.
                if (!builder.IsValidAuthentication(builder.Connection!.Resource!))
                {
                    // Get authentication token result using "IDynamicsAuthenticator" service.
                    var authentication = authenticator.AuthenticateAsync().Result;

                    // Add authorization if success authentication.
                    if (authentication != null && !string.IsNullOrEmpty(authentication.AccessToken))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authentication.TokenType, authentication.AccessToken);
                }
                else
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(builder.Authentication!.TokenType, builder.Authentication!.AccessToken);
                }
                
                // Set authentication token according to DynamicsBuilder information.
                client.BaseAddress = new Uri($"{builder.Connection!.Resource!}/api/data/v{(builder.Connection!.Version == 0.0 ? "9.2" : builder.Connection!.Version)}/");
                return client;
            } 
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the configuration of the service client for Dynamics", ex);
            }
        }

        /// <summary>
        /// Initialize default BaseAddress configuration for a client to consume Dynamics API.
        /// </summary>
        /// <param name="client">Dynamics http client.</param>
        internal static void ConfigureDefaultClientConfiguration(this HttpClient client)
        {
            // Configure default client configuration.
            client.Timeout = new TimeSpan(0, 2, 0);
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            client.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=\"*\"");
            client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };
        }
    }
}