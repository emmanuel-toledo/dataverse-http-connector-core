using Microsoft.Identity.Client;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions.Dataverse;

namespace Dataverse.Http.Connector.Core.Business.Authentication
{
    /// <summary>
    /// This class defines Dataverse authentication method using Application User configuratin.
    /// </summary>
    internal interface IDataverseAuthenticator
    {
        /// <summary>
        /// Function to get Authentication Result.
        /// </summary>
        /// <exception cref="UnauthorizedException">The configuration connection is not authorized.</exception>
        /// <returns>Microsoft identity client authentication result</returns>
        Task<AuthenticationResult> AuthenticateAsync();
    }
}
