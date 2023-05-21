namespace Dynamics.Crm.Http.Connector.Core.Models.Access
{
    /// <summary>
    /// This class contains the deffinition of a Authentication Token for Dynamics.
    /// </summary>
    [Obsolete("Replaced for Microsoft.Identity.Client.AuthenticationResult class.")]
    internal class AuthenticationToken
    {
        /// <summary>
        /// Get and Set Authentication token type.
        /// </summary>
        public string? TokenType { get; set; } = string.Empty;

        /// <summary>
        /// Get and Set Authentication token expires in.
        /// </summary>
        public string? ExpiresIn { get; set; } = string.Empty;

        /// <summary>
        /// Get and Set Authentication token ext, expires in.
        /// </summary>
        public string? ExtExpiresIn { get; set; } = string.Empty;

        /// <summary>
        /// Get and Set Authentication token expires on.
        /// </summary>
        public string? ExpiresOn { get; set; } = string.Empty;

        /// <summary>
        /// Get and Set Authentication token not before.
        /// </summary>
        public string? NotBefore { get; set; } = string.Empty;

        /// <summary>
        /// Get and Set Authentication token resouce.
        /// </summary>
        public string? Resource { get; set; } = string.Empty;

        /// <summary>
        /// Get and Set Authentication token value.
        /// </summary>
        public string? AccessToken { get; set; } = string.Empty;
    }
}
