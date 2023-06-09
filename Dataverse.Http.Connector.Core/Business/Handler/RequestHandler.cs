using Microsoft.Extensions.Logging;
using Dataverse.Http.Connector.Core.Persistence;
using Dataverse.Http.Connector.Core.Business.Queries;
using Dataverse.Http.Connector.Core.Business.Commands;
using Dataverse.Http.Connector.Core.Domains.Dataverse.Context;
using Dataverse.Http.Connector.Core.Extensions.DependencyInjections.Configurations;

namespace Dataverse.Http.Connector.Core.Business.Handler
{
    /// <summary>
    /// This interface defines the main request operations for Dataverse.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Function to retrive a new TEntity class instance.
        /// <para>
        /// The return value can be null if the record does not exists.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity instance.</returns>
        Task<TEntity> FirstAsync<TEntity>(Action<Request> action) where TEntity : class, new();

        /// <summary>
        /// Function to retrive a new TEntity class instance.
        /// <para>
        /// The return value can be null if the record does not exists.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity instance or null value.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(Action<Request> action) where TEntity : class, new();

        /// <summary>
        /// Function to retrive a collection of TEntity class.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity collection instance.</returns>
		Task<ICollection<TEntity>> ToListAsync<TEntity>(Action<Request> action) where TEntity : class, new();

        /// <summary>
        /// Function to retrive a collection of TEntity class in a paged format.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <param name="currentPage">Current page to retrive.</param>
        /// <param name="pageSize">Max records count per page.</param>
        /// <param name="totalElements">Total records count.</param>
        /// <returns>Paged response of type TEntity.</returns>
        Task<PagedResponse<TEntity>> ToPagesAsync<TEntity>(Action<Request> action, int currentPage, int pageSize, int totalElements) where TEntity : class, new();

        /// <summary>
        /// Function to retrive a count of records of an entity in Dataverse.
        /// </summary>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>Records count.</returns>
        Task<int> CountAsync(Action<Request> action);

        /// <summary>
        /// Function to create a new entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <param name="entity">TEntity instance record.</param>
        /// <returns>New TEntity instance or null value.</returns>
        Task<TEntity?> AddAsync<TEntity>(Action<Request> action, TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Function to update an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <param name="entity">TEntity instance record.</param>
        /// <returns>TEntity instance or null value.</returns>
        Task<TEntity?> UpdateAsync<TEntity>(Action<Request> action, TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Function to delete an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <param name="entity">TEntity instance record.</param>
        /// <returns>TEntity instance or null value.</returns>
        Task<TEntity?> DeleteAsync<TEntity>(Action<Request> action, TEntity entity) where TEntity : class, new();
    }

    /// <summary>
    /// This class implements the main request operations for Dataverse.
    /// </summary>
    internal class RequestHandler : IRequestHandler
    {
        /// <summary>
        /// Private Dataverse queries service.
        /// </summary>
        private readonly IDataverseQueries _queries;

        /// <summary>
        /// Private Dataverse commands service.
        /// </summary>
        private readonly IDataverseCommands _commands;

        /// <summary>
        /// Private logger service.
        /// </summary>
        private readonly ILogger<IDataverseContext> _logger;

        /// <summary>
        /// Generates a new instance of Request Handler service.
        /// </summary>
        /// <param name="queries">Dataverse queries service.</param>
        /// <param name="commands">Dataverse commands service.</param>
        public RequestHandler(IDataverseQueries queries, IDataverseCommands commands, ILogger<IDataverseContext> logger)
        {
            _queries = queries;
            _commands = commands;
            _logger = logger;
        }

        /// <summary>
        /// Function to generate a new http request message from an "Request" model instance.
        /// </summary>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New http request message.</returns>
		private static HttpRequestMessage GenerateHttpRequest(Action<Request> action)
		{
			Request request = new();
			action(request);
			return request.ConvertToHttpRequest();
		}

        /// <summary>
        /// Function to generate a new "Request" model instance.
        /// </summary>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>Dataverse request object.</returns>
		private static Request GenerateRequest(Action<Request> action)
        {
            Request request = new();
            action(request);
            return request;
        }

        /// <summary>
        /// Function to retrive a new TEntity class instance.
        /// <para>
        /// The return value can be null if the record does not exists.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity instance.</returns>
        public async Task<TEntity> FirstAsync<TEntity>(Action<Request> action) where TEntity : class, new()
        {
            var request = GenerateRequest(action);
            _logger.LogInformation(request.FetchXml);
            return await _queries.FirstAsync<TEntity>(GenerateHttpRequest(action));
        }

        /// <summary>
        /// Function to retrive a new TEntity class instance.
        /// <para>
        /// The return value can be null if the record does not exists.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity instance or null value.</returns>
        public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(Action<Request> action) where TEntity : class, new()
        {
            var request = GenerateRequest(action);
            _logger.LogInformation(request.FetchXml);
            return await _queries.FirstOrDefaultAsync<TEntity>(GenerateHttpRequest(action));
        }

        /// <summary>
        /// Function to retrive a collection of TEntity class.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity collection instance.</returns>
		public async Task<ICollection<TEntity>> ToListAsync<TEntity>(Action<Request> action) where TEntity : class, new()
        {
            var request = GenerateRequest(action);
            _logger.LogInformation(request.FetchXml);
            return await _queries.ToListAsync<TEntity>(GenerateHttpRequest(action));
        }

        /// <summary>
        /// Function to retrive a collection of TEntity class in a paged format.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <param name="currentPage">Current page to retrive.</param>
        /// <param name="pageSize">Max records count per page.</param>
        /// <param name="totalElements">Total records count.</param>
        /// <returns>Paged response of type TEntity.</returns>
        public async Task<PagedResponse<TEntity>> ToPagesAsync<TEntity>(Action<Request> action, int currentPage, int pageSize, int totalElements) where TEntity : class, new()
        {
            var collection = await ToListAsync<TEntity>(action);
            var pagedResponse = new PagedResponse<TEntity>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                PagesCount = (int)Math.Ceiling((double)totalElements / pageSize),
                RowCount = collection.Count,
                Results = collection.ToList()
            };
            return pagedResponse;
        }

        /// <summary>
        /// Function to retrive a count of records of an entity in Dataverse.
        /// </summary>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>Records count.</returns>
        public async Task<int> CountAsync(Action<Request> action)
            => await _queries.CountAsync(GenerateHttpRequest(action));

        /// <summary>
        /// Function to create a new entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>New TEntity instance or null value.</returns>
        public async Task<TEntity?> AddAsync<TEntity>(Action<Request> action, TEntity entity) where TEntity : class, new()
            => await _commands.AddAsync(GenerateHttpRequest(action), entity);

        /// <summary>
        /// Function to update an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <returns>TEntity instance or null value.</returns>
        public async Task<TEntity?> UpdateAsync<TEntity>(Action<Request> action, TEntity entity) where TEntity : class, new()
            => await _commands.UpdateAsync(GenerateHttpRequest(action), entity);

        /// <summary>
        /// Function to delete an entity record in Dataverse.
        /// </summary>
        /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
        /// <param name="action">Action of type "Request" model.</param>
        /// <param name="entity">TEntity instance record.</param>
        /// <returns>TEntity instance or null value.</returns>
        public async Task<TEntity?> DeleteAsync<TEntity>(Action<Request> action, TEntity entity) where TEntity : class, new()
            => await _commands.DeleteAsync(GenerateHttpRequest(action), entity);
    }
}
