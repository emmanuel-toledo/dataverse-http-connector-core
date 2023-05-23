namespace  Dynamics.Crm.Http.Connector.Core.Models.Configurations
{
    /// <summary>
    /// This class contains main information for Dynamics Connection using HTTP Client.
    /// </summary>
    public class DynamicsConnection
    {
        /// <summary>
        /// Get and set Connection unique identifier.
        /// </summary>
        internal Guid ConnectionId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Get and set Connection name.
        /// </summary>
        public string? ConnectionName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set Tenant unique identifier.
        /// </summary>
        public Guid TenantId { get; set; } = Guid.Empty;

        /// <summary>
        /// Get and set Client unique identifier.
        /// </summary>
        public Guid ClientId { get; set; } = Guid.Empty;

        /// <summary>
        /// Get and set Client Secret.
        /// </summary>
        public string? ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// Get and set REST API Version.
        /// </summary>
        public double Version { get; set; } = 0.0;

        /// <summary>
        /// Get and set Dynamics environment URL.
        /// </summary>
        public string? Resource { get; set; } = string.Empty;
    }
}
