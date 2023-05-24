using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace  Dynamics.Crm.Http.Connector.Core.Models.Context
{
    public class DbEntitySet<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>, IListSource where TEntity : class
    {
        /// <summary>
        /// Gets a value indicating whether the collection is a collection of System.Collections.IList objects.
        /// Always returns <see langword="false" />.
        /// </summary>
        bool IListSource.ContainsListCollection 
            => false;

        /// <summary>
        /// Gets the IQueryable element type.
        /// </summary>
        Type IQueryable.ElementType 
            => throw new NotImplementedException();

        /// <summary>
        /// Gets the IQueryable LINQ Expression.
        /// </summary>
        Expression IQueryable.Expression 
            => throw new NotImplementedException();

        /// <summary>
        /// Gets the IQueryable provider.
        /// </summary>
        IQueryProvider IQueryable.Provider 
            => throw new NotImplementedException();

        /// <summary>
        /// Returns an <see cref="IAsyncEnumerator{T}" /> which when enumerated will asynchronously execute a query against
        /// the database.
        /// </summary>
        /// <returns> The query results. </returns>
        IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        /// <summary>
        /// Returns an <see cref="IEnumerator{T}" /> which when enumerated will execute a query against the database
        /// to load all entities from the database.
        /// </summary>
        /// <returns> The query results. </returns>
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
            => throw new NotImplementedException();

        /// <summary>
        /// <para>
        /// This method is called by data binding frameworks when attempting to data bind
        /// directly to a <see cref="DbEntitySet{TEntity}" />.
        /// </para>
        /// </summary>
        /// <exception cref="NotSupportedException"> Always thrown. </exception>
        /// <returns> Never returns, always throws an exception. </returns>
        IList IListSource.GetList()
            => throw new NotSupportedException();

        /// <summary>
        ///     Returns an <see cref="IEnumerator" /> which when enumerated will execute a query against the database
        ///     to load all entities from the database.
        /// </summary>
        /// <returns> The query results. </returns>
        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException();
    }
}
