using Dynamics.Crm.Http.Connector.Core.Models.Context;
using  Dynamics.Crm.Http.Connector.Core.Persistence;

namespace  Dynamics.Crm.Http.Connector.Core
{
    public class DynamicsContext : IDynamicsContext
    {
        public virtual DbEntitySet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}