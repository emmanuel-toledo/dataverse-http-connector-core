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
                    TenantId = new Guid("346a1d1d-e75b-4753-902b-74ed60ae77a1"),
                    ClientId = new Guid("5f32110f-cf6e-4557-9e40-391ff6870b12"),
                    ClientSecret = "-~E1gn3-40G7ZDu_jdzD68~Uyb~U9CB~7s",
                    Resource = "https://latammx-caeuvmdesarrollo.crm.dynamics.com",
                    ConnectionName = "LATAMMX-DEVELOP",
                };
            }
        }
    }
}
