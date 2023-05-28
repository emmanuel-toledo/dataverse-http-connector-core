using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Builder
{
    public class FetchXmlBuilder<TEntity> where TEntity : class, new()
    {
        private List<FilterBuilder<TEntity>> _filters = new();
        private List<FetchXmlBuilder<TEntity>> _linkEntities = new();

        public FetchXmlBuilder<TEntity> AddFilter(FilterBuilder<TEntity> filter)
        {
            _filters.Add(filter);
            return this;
        }

        internal string Build()
        {

            return ""; // Construir XML.
        }
    }
}
