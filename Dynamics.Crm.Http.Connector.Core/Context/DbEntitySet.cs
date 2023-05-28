using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Context
{
    public class DbEntitySet<TEntity> where TEntity : class, new()
    {
        private FetchXmlBuilder<TEntity> fetch = new();

        public DbEntitySet<TEntity> Filter(FilterTypes type, Action<FilterBuilder<TEntity>> action)
        {
            try
            {
                FilterBuilder<TEntity> filter = Instance<TEntity>.FilterBuilderInstance(type);
                action(filter);
                fetch.AddFilter(filter);
                return this;
            } catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }

        public DbEntitySet<TEntity> FilterAnd(Action<FilterBuilder<TEntity>> action)
            => Filter(FilterTypes.And, action);

        public DbEntitySet<TEntity> FilterOr(Action<FilterBuilder<TEntity>> action)
            => Filter(FilterTypes.Or, action);

        //public TEntity FirstOrDefault()
        //{
        //    return this;
        //}

        //public ICollection<TEntity> ToList()
        //{
        //    fetch.Build();
        //    return this;
        //}
    }
}
