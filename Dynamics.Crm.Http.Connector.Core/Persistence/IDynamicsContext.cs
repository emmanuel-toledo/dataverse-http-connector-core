using Dynamics.Crm.Http.Connector.Core.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Dynamics.Crm.Http.Connector.Core.Persistence
{
    public interface IDynamicsContext
    {
        DbEntitySet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
