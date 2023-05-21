using Microsoft.Identity.Client;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Exceptions;

namespace  Dynamics.Crm.Http.Connector.Core.Business.Authentication
{
    /// <summary>
    /// This class implements Dynamics CRM Authentication method using Application User configuratin.
    /// </summary>
    internal class DynamicsAuthenticator : IDynamicsAuthenticator
    {
        /// <summary>
        /// Function to get Authentication Result.
        /// </summary>
        /// <param name="connection">Dynamics configuration model.</param>
        /// <exception cref="UnauthorizedException">The configuration connection is not authorized.</exception>
        /// <returns>Microsoft identity client authentication result</returns>
        public async Task<AuthenticationResult> AuthenticateAsync(DynamicsConnection connection)
        {
            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(connection.ClientId.ToString())
                    .WithAuthority(AzureCloudInstance.AzurePublic, connection.TenantId)
                    .WithClientSecret(connection.ClientSecret)
                    .Build();
                var acquireToken = await app.AcquireTokenForClient(new string[] { $"{connection.Resource}/.default" }).ExecuteAsync();
                return acquireToken is null ? throw new UnauthorizedException((object)connection.Resource!) : acquireToken;
            }
            catch
            {
                throw;
            }
        }
    }
}
