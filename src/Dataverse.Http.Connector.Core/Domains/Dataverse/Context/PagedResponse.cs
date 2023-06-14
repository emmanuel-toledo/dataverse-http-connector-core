namespace Dataverse.Http.Connector.Core.Domains.Dataverse.Context
{
	/// <summary>
	/// This class contains the properties for a paged response.
	/// </summary>
	/// <typeparam name="TResponse">List type to retrive.</typeparam>
	public class PagedResponse<TResponse> where TResponse : class
	{
		/// <summary>
		/// Get and set the current page.
		/// </summary>
		public int CurrentPage { get; set; }

		/// <summary>
		/// Get and set the page size.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Get and set the Page count.
		/// </summary>
		public int PagesCount { get; set; }

		/// <summary>
		/// Get and set the row count.
		/// </summary>
		public long RowCount { get; set; }

		/// <summary>
		/// Get and set the results.
		/// </summary>
		public ICollection<TResponse> Results { get; set; } = new HashSet<TResponse>();
	}
}
