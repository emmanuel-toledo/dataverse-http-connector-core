using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context;

namespace Dynamics.Crm.Http.Connector.Core.Context
{
    /// <summary>
    /// This interface define all the functions that can be used with this library.
    /// </summary>
    /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
    public interface IDbEntitySet<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Function to add a new Filter of type "and" to the FetchXml query.
        /// </summary>
        /// <param name="action">Action configuration of type "FilterBuilder"</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        DbEntitySet<TEntity> FilterAnd(Action<FilterBuilder<TEntity>> action);

        /// <summary>
        /// Function to add a new Filter of type "or" to the FetchXml query.
        /// </summary>
        /// <param name="action">Action configuration of type "FilterBuilder"</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        DbEntitySet<TEntity> FilterOr(Action<FilterBuilder<TEntity>> action);

        /// <summary>
        /// Function to add a "top" tag to FetchXml query.
        /// </summary>
        /// <param name="top">Top count records to retrive.</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        DbEntitySet<TEntity> Top(int top);

        /// <summary>
        /// Function to add a "distinct" tag to FetchXml query.
        /// </summary>
        /// <param name="distinct">Distinct records to retrive.</param>
        /// <returns>Instance of "DbEntitySet" class.</returns>
        DbEntitySet<TEntity> Distinct(bool distinct);

        /// <summary>
        /// Function to retrive first or default "TEntity" record according to defined FetchXml query.
        /// </summary>
        /// <returns>New instance of "TEntity".</returns>
        Task<TEntity?> FirstOrDefaultAsync();

        /// <summary>
        /// Function to retrive a collection of "TEntity" records according to defined FetchXml query.
        /// </summary>
        /// <returns>Collection of "TEntity".</returns>
        Task<ICollection<TEntity>> ToListAsync();

		/// <summary>
		/// Function to retrive a collection of "TEntity" records with pagination configuration according to defined FetchXml query.
		/// </summary>
		/// <param name="currentPage">Page to retrive.</param>
		/// <param name="pageSize">Max records count for each page.</param>
		/// <returns>Paged response model of type "TEntity".</returns>
		Task<PagedResponse<TEntity>> ToPagedListAsync(int currentPage, int pageSize);

		/// <summary>
		/// Function to add a new "TEntity" record to the database.
		/// </summary>
		/// <returns>Instance of "TEntity".</returns>
		Task<TEntity> AddAsync();

        /// <summary>
        /// Function to update a new "TEntity" record to the database.
        /// </summary>
        /// <returns>Instance of "TEntity".</returns>
        Task<TEntity> UpdateAsync();

        /// <summary>
        /// Function to delete a new "TEntity" record to the database.
        /// </summary>
        /// <returns>Instance of "TEntity".</returns>
        Task<TEntity> DeleteAsync();
    }
}
