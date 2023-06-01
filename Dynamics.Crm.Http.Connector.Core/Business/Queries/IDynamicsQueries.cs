using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Business.Queries
{
    public interface IDynamicsQueries
    {
		Task<TEntity?> FirstOrDefaultAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new();

		Task<ICollection<TEntity>> ToListAsync<TEntity>(HttpRequestMessage requestMessage) where TEntity : class, new();
	}
}
