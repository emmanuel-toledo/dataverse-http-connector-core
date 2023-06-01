using Dynamics.Crm.Http.Connector.Core.Business.Commands;
using Dynamics.Crm.Http.Connector.Core.Business.Handler;
using Dynamics.Crm.Http.Connector.Core.Business.Queries;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using Dynamics.Crm.Http.Connector.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Context
{
    public class DbEntitySet<TEntity> : IDbEntitySet<TEntity> where TEntity : class, new()
    {
        private readonly IRequestHandler _requestHandler;

        private FetchXmlBuilder<TEntity> _fetchBuilder = new();

        public DbEntitySet(IRequestHandler requestHandler)
        {
            _fetchBuilder = new();
			_requestHandler = requestHandler;
		}

        /// <summary>
        /// Function to add a new Filter to the FetchXml builder for request query.
        /// </summary>
        /// <param name="type">Filter type</param>
        /// <param name="action">Action configuration of type "FilterBuilder"</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        /// <exception cref="NotSupportedException">The "TEntity" defined does not contains all required configuration.</exception>
        internal DbEntitySet<TEntity> Filter(FilterTypes type, Action<FilterBuilder<TEntity>> action)
        {
            try
            {
                FilterBuilder<TEntity> filter = Instance<TEntity>.FilterBuilderInstance(type);
                action(filter);
                _fetchBuilder.AddFilter(filter);
                return this;
            }
            catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Function to add a new Filter of type "and" to the FetchXml query.
        /// </summary>
        /// <param name="action">Action configuration of type "FilterBuilder"</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        public DbEntitySet<TEntity> FilterAnd(Action<FilterBuilder<TEntity>> action)
            => Filter(FilterTypes.And, action);

        /// <summary>
        /// Function to add a new Filter of type "or" to the FetchXml query.
        /// </summary>
        /// <param name="action">Action configuration of type "FilterBuilder"</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        public DbEntitySet<TEntity> FilterOr(Action<FilterBuilder<TEntity>> action)
            => Filter(FilterTypes.Or, action);

        /// <summary>
        /// Function to add a "top" tag to FetchXml query.
        /// </summary>
        /// <param name="top">Top count records to retrive.</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        public DbEntitySet<TEntity> Top(int top)
        {
            _fetchBuilder.AddTop(top);
            return this;
        }

        /// <summary>
        /// Function to add a "distinct" tag to FetchXml query.
        /// </summary>
        /// <param name="distinct">Distinct records to retrive.</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        public DbEntitySet<TEntity> Distinct(bool distinct)
        {
            _fetchBuilder.AddDistinct(distinct);
            return this;
        }


        // TODO:


        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <returns>New instance of "TEntity".</returns>
        public async Task<TEntity?> FirstOrDefaultAsync()
        {
            _fetchBuilder.FirstOrDefault();
            return await _requestHandler.FirstOrDefaultAsync<TEntity>(request =>
			{
				request.MethodType = HttpMethod.Get;
				request.EndPoint = Annotations.GetEntityAttributes(Instance<TEntity>.TEntityInstance().GetType())!.LogicalName;
				request.AddParam("fetchXml", BuildFetchXml());
			});
		}

        /// <summary>
        /// Function to retrive a collection of "TEntity" records according to defined FetchXml query.
        /// </summary>
        /// <returns>Collection of "TEntity".</returns>
        public async Task<ICollection<TEntity>> ToListAsync()
        {
            return await _requestHandler.ToListAsync<TEntity>(request =>
            {
				request.MethodType = HttpMethod.Get;
				request.EndPoint = Annotations.GetEntityAttributes(Instance<TEntity>.TEntityInstance().GetType())!.LogicalName;
				request.AddParam("fetchXml", BuildFetchXml());
			});
        }

		/// <summary>
		/// Function to retrive a collection of "TEntity" records with pagination configuration according to defined FetchXml query.
		/// </summary>
		/// <returns>Collection of "TEntity".</returns>
		public async Task<PagedResponse<TEntity>> ToPagedListAsync(int currentPage, int pageSize)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Function to add a new "TEntity" record to the database.
		/// </summary>
		/// <returns>Instance of "TEntity".</returns>
		public async Task<TEntity> AddAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to update a new "TEntity" record to the database.
        /// </summary>
        /// <returns>Instance of "TEntity".</returns>
        public async Task<TEntity> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to delete a new "TEntity" record to the database.
        /// </summary>
        /// <returns>Instance of "TEntity".</returns>
        public async Task<TEntity> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        internal string BuildFetchXml()
            => _fetchBuilder.BuildFetchXml();
    }
}
