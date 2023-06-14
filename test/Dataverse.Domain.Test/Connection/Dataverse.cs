using Dataverse.Http.Connector.Core.Domains.Dataverse.Connection;

namespace Dataverse.Domain.Test.Connection
{
    public static class Dataverse
    {
        public static DataverseConnection Connection
        {
            get
            {
                return new()
                {
                    TenantId = Guid.Empty,
                    ClientId = Guid.Empty,
                    ClientSecret = "your secret",
                    Resource = "https://contoso.crm.dynamics.com",
                    ConnectionName = "CONTOSO-DEVELOP",
                };
            }
        }
    }
}
