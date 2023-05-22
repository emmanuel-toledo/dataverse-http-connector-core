using Microsoft.Identity.Client;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dynamics.Crm.Http.Connector.Core.Business.Authentication
{
    /// <summary>
    /// This class implements Dynamics CRM Authentication method using Application User configuratin.
    /// </summary>
    // internal class DynamicsAuthenticator : IDynamicsAuthenticator
    public class DynamicsAuthenticator : IDynamicsAuthenticator
    {
        /// <summary>
        /// Private established Dynamics Connection.
        /// </summary>
        private readonly DynamicsConnection _connection;

        /// <summary>
        /// Initialize a new instance of Authentication Service.
        /// </summary>
        /// <param name="builder">IDynamicsBuilder service from DI.</param>
        /// <exception cref="ArgumentNullException">Dynamics connection is null.</exception>
        public DynamicsAuthenticator(IDynamicsBuilder builder)
            => _connection = builder.Connection ?? throw new ArgumentNullException("Make sure that you have already a connection configured");

        /// <summary>
        /// Function to get Authentication Result.
        /// </summary>
        /// <param name="connection">Dynamics configuration model.</param>
        /// <exception cref="UnauthorizedException">The configuration connection is not authorized.</exception>
        /// <returns>Microsoft identity client authentication result</returns>
        public async Task<AuthenticationResult> AuthenticateAsync()
        {
            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(_connection.ClientId.ToString())
                    .WithAuthority(AzureCloudInstance.AzurePublic, _connection.TenantId)
                    .WithClientSecret(_connection.ClientSecret)
                    .Build();
                var acquireToken = await app.AcquireTokenForClient(new string[] { $"{_connection.Resource}/.default" }).ExecuteAsync();
                return acquireToken is null ? throw new UnauthorizedException((object)_connection.Resource!) : acquireToken;
            }
            catch
            {
                throw;
            }
        }
    }
}
