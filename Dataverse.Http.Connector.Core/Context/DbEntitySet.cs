using Dataverse.Http.Connector.Core.Utilities;
using Dataverse.Http.Connector.Core.Domains.Enums;
using Dataverse.Http.Connector.Core.Domains.Builder;
using Dataverse.Http.Connector.Core.Business.Handler;
using Dataverse.Http.Connector.Core.Domains.Annotations;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Context;

namespace Dataverse.Http.Connector.Core.Context
{
    /// <summary>
    /// This class implements all the functions that can be used with this library.
    /// </summary>
    /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
    public class DbEntitySet<TEntity> : IDbEntitySet<TEntity> where TEntity : class, new()
    {
        private readonly IRequestHandler _requestHandler;

        private readonly IDataverseBuilder _builder;

        private FetchXmlBuilder<TEntity> _fetchBuilder = new();

        public DbEntitySet(IRequestHandler requestHandler, IDataverseBuilder builder)
        {
            _fetchBuilder = new();
			_requestHandler = requestHandler;
            _builder = builder;
		}

        /// <summary>
        /// Function to build FetchXml query for Dataverse.
        /// </summary>
        /// <returns>FetchXml query string.</returns>
        private string BuildFetchXml()
            => _fetchBuilder.BuildFetchXml(
                _builder.GetEntityAttributesFromType(typeof(TEntity)),
                _builder.GetFieldsAttributesFromType(typeof(TEntity)));

        /// <summary>
        /// Function to generate FetchXml query to show in Logger.
        /// </summary>
        /// <returns>FetchXml query string.</returns>
        private string BuildLoggerFetchXml()
            => _fetchBuilder.BuildLoggerFetchXml(
                _builder.GetEntityAttributesFromType(typeof(TEntity)),
                _builder.GetFieldsAttributesFromType(typeof(TEntity)));

        /// <summary>
        /// Function to build count FetchXml query for Dataverse.
        /// </summary>
        /// <returns>FetchXml query string.</returns>
        private string BuildCountFetchXml()
            => _fetchBuilder.BuildCountFetchXml(
                _builder.GetEntityAttributesFromType(typeof(TEntity)),
                _builder.GetFieldsAttributesFromType(typeof(TEntity)));

        /// <summary>
        /// Function to generate the endpont to call a request.
        /// </summary>
        /// <returns>Endpont string.</returns>
        private string BuildEndpoint()
        {
            var entity = _builder.Entities.First(x => x.EntityType == typeof(TEntity));
            return $"{entity.EntityAttributes!.LogicalCollectionName}";
        }

        /// <summary>
        /// Function to generate the endpont to call a request setting a TEntity unique identifier.
        /// </summary>
        /// <param name="entity">TEntity instance.</param>
        /// <returns>Endpont string.</returns>
        private string BuildEndpoint(TEntity entity)
        {
            var entityDeffinition = _builder.Entities.First(x => x.EntityType == entity.GetType());
            if (!entityDeffinition.FieldsAttributes.Any(x => x.FieldType == FieldTypes.UniqueIdentifier))
                throw new EntityFieldDeffinitionException($"Entity with name '{ entity.GetType().Name }' does not contains defined a unique identifier attribute");
            var primaryKey = entityDeffinition.FieldsAttributes.First(x => x.FieldType == FieldTypes.UniqueIdentifier);
            return $"{entityDeffinition.EntityAttributes!.LogicalCollectionName}({entity.GetType().GetProperty(primaryKey.TEntityPropertyName)!.GetValue(entity)})";
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

        /// <summary>
        /// Function to add a "page" tag to FetchXml query.
        /// </summary>
        /// <param name="page">Page records to retrive.</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        public DbEntitySet<TEntity> Page(int page)
        {
            _fetchBuilder.AddPage(page);
            return this;
        }

        /// <summary>
        /// Function to add a "page size" tag to FetchXml query.
        /// </summary>
        /// <param name="pageSize">Page size records to retrive.</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        public DbEntitySet<TEntity> PageSize(int pageSize)
        {
            _fetchBuilder.AddPageSize(pageSize);
            return this;
        }

        /// <summary>
        /// Function to retrive first "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <returns>New instance of "TEntity".</returns>
        public async Task<TEntity> FirstAsync()
        {
            _fetchBuilder.FirstOrDefault();
            return await _requestHandler.FirstAsync<TEntity>(request =>
            {
                request.Method = BaseMethodTypes.FirstAsync;
                request.EndPoint = BuildEndpoint();
                request.AddQueryParam(BuildFetchXml());
                request.FetchXml = BuildLoggerFetchXml();
            });
        }

        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <returns>New instance of "TEntity".</returns>
        public async Task<TEntity?> FirstOrDefaultAsync()
        {
            _fetchBuilder.FirstOrDefault();
            return await _requestHandler.FirstOrDefaultAsync<TEntity>(request =>
            {
                request.Method = BaseMethodTypes.FirstOrDefaultAsync;
                request.EndPoint = BuildEndpoint();
                request.AddQueryParam(BuildFetchXml());
                request.FetchXml = BuildLoggerFetchXml();
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
                request.Method = BaseMethodTypes.ToListAsync;
                request.EndPoint = BuildEndpoint();
                request.AddQueryParam(BuildFetchXml());
                request.FetchXml = BuildLoggerFetchXml();
            });
        }

        /// <summary>
        /// Function to retrive the count records of an entity according to defined FetchXml query.
        /// </summary>
        /// <returns>Records count.</returns>
        public async Task<int> CountAsync()
        {
            return await _requestHandler.CountAsync(request =>
            {
                request.Method = BaseMethodTypes.CountAsync;
                request.EndPoint = BuildEndpoint();
                request.AddQueryParam(BuildCountFetchXml());
            });
        }

        /// <summary>
        /// Function to retrive a collection of "TEntity" records with pagination configuration according to defined FetchXml query.
        /// </summary>
        /// <returns>Collection of "TEntity".</returns>
        public async Task<PagedResponse<TEntity>> ToPagedListAsync(int currentPage = 1, int pageSize = 15)
        {
            Page(currentPage);
            PageSize(pageSize);
            return await _requestHandler.ToPagesAsync<TEntity>(request =>
            {
                request.Method = BaseMethodTypes.ToPagedListAsync;
                request.EndPoint = BuildEndpoint();
                request.AddQueryParam(BuildFetchXml());
                request.FetchXml = BuildLoggerFetchXml();
            }, currentPage, pageSize, await CountAsync());
        }

        /// <summary>
        /// Function to add a new "TEntity" record to the database.
        /// </summary>
        /// <param name="entity">Entity record.</param>
        /// <returns>Instance of "TEntity".</returns>
        public async Task<TEntity?> AddAsync(TEntity entity)
        {
            return await _requestHandler.AddAsync(request =>
            {
                request.Method = BaseMethodTypes.AddAsync;
                request.EndPoint = BuildEndpoint();
            }, entity);
        }

        /// <summary>
        /// Function to update a new "TEntity" record to the database.
        /// </summary>
        /// <param name="entity">Entity record.</param>
        /// <returns>Instance of "TEntity".</returns>
        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            return await _requestHandler.UpdateAsync(request =>
            {
                request.Method = BaseMethodTypes.UpdateAsync;
                request.EndPoint = BuildEndpoint(entity);
            }, entity);
        }

        /// <summary>
        /// Function to delete a new "TEntity" record to the database.
        /// </summary>
        /// <returns>Instance of "TEntity".</returns>
        public async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            return await _requestHandler.DeleteAsync(request =>
            {
                request.Method = BaseMethodTypes.DeleteAsync;
                request.EndPoint = BuildEndpoint(entity);
            }, entity);
        }
    }
}
