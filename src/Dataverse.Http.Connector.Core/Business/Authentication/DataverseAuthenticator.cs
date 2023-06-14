using Microsoft.Identity.Client;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse;

namespace Dataverse.Http.Connector.Core.Business.Authentication
{
    /// <summary>
    /// This class implements Dataverse Authentication method using Application User configuratin.
    /// </summary>
    public class DataverseAuthenticator : IDataverseAuthenticator
    {
        /// <summary>
        /// Private established Dataverse Connection.
        /// </summary>
        private readonly DataverseConnection _connection;

        /// <summary>
        /// Private Dataverse builder configuration.
        /// </summary>
        private readonly IDataverseBuilder _builder;

        /// <summary>
        /// Initialize a new instance of Authentication Service.
        /// </summary>
        /// <param name="builder">IDataverseBuilder service from DI.</param>
        /// <exception cref="ArgumentNullException">Dataverse connection is null.</exception>
        public DataverseAuthenticator(IDataverseBuilder builder)
        {
            _builder = builder;
            _connection = _builder.Connection ?? throw new ArgumentNullException("Make sure that you have already a connection configured");
        }

        /// <summary>
        /// Function to get Authentication Result.
        /// </summary>
        /// <exception cref="UnauthorizedException">The configuration connection is not authorized.</exception>
        /// <returns>Microsoft identity client authentication result</returns>
        public async Task<AuthenticationResult> AuthenticateAsync()
        {
            try
            {
                // Create confidential connection to get access token.
                var app = ConfidentialClientApplicationBuilder.Create(_connection.ClientId.ToString())
                    .WithAuthority(AzureCloudInstance.AzurePublic, _connection.TenantId)
                    .WithClientSecret(_connection.ClientSecret)
                    .Build();
                // Execute request to access retrieve token.
                var acquireToken = await app.AcquireTokenForClient(new string[] { $"{_connection.Resource}/.default" }).ExecuteAsync();
                // Save Authentication Result inside builder service to cached token.
                _builder.SetAuthenticationResult(acquireToken);
                // Return value or throw exception if result is null.
                return acquireToken is null ? throw new UnauthorizedException((object)_connection.Resource!) : acquireToken;
            }
            catch
            {
                throw;
            }
        }
    }
}
