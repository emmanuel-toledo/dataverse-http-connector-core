using Dynamics.Crm.Http.Connector.Core.Models.Configurations;

namespace Dynamics.Crm.Http.Connector.Core.UT
{
    public static class Dynamics
    {
        public static DynamicsConnection Connection
        {
            get
            {
                return new DynamicsConnection
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
