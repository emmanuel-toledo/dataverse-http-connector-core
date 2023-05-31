using Dynamics.Crm.Http.Connector.Core.Context;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class Instance<TEntity> where TEntity : class, new()
    {
        public static TEntity TEntityInstance()
            => (TEntity)Activator.CreateInstance(typeof(TEntity))!;

        public static DbEntitySet<TEntity> DbEntitySetInstance()
            => (DbEntitySet<TEntity>)Activator.CreateInstance(typeof(DbEntitySet<TEntity>))!;

        public static FilterBuilder<TEntity> FilterBuilderInstance(FilterTypes type)
            => (FilterBuilder<TEntity>)Activator.CreateInstance(typeof(FilterBuilder<TEntity>), type)!;
    }
}
