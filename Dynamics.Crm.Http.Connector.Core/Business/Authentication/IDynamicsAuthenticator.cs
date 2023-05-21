using Microsoft.Identity.Client;
using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Exceptions;

namespace Dynamics.Crm.Http.Connector.Core.Business.Authentication
{
    /// <summary>
    /// This class defines Dynamics CRM Authentication method using Application User configuratin.
    /// </summary>
    internal interface IDynamicsAuthenticator
    {
        /// <summary>
        /// Function to get Authentication Result.
        /// </summary>
        /// <param name="connection">Dynamics configuration model.</param>
        /// <exception cref="UnauthorizedException">The configuration connection is not authorized.</exception>
        /// <returns>Microsoft identity client authentication result</returns>
        Task<AuthenticationResult> AuthenticateAsync(DynamicsConnection connection);
    }
}
