using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;

namespace Dataverse.Http.Connector.Core.UT
{
    public static class Dynamics
    {
        public static DataverseConnection Connection
        {
            get
            {
                return new DataverseConnection
                {
                    TenantId = Guid.Empty,
                    ClientId = Guid.Empty,
                    ClientSecret = "Secret",
                    Resource = "Dynamics URL",
                    ConnectionName = "Custom connection name",
                };
            }
        }
    }
}
