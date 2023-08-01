using System.Linq.Expressions;
using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Http.Connector.Core.Utilities
{
    /// <summary>
    /// This class utility functions to check if the information of the "FilterBuilder" class is correctly.
    /// </summary>
    internal static class FilterBuilderUtilities
    {
        /// <summary>
        /// Check if the expression is correctly created and configured to get "Entity" attributes.
        /// </summary>
        /// <typeparam name="TEntity">Generic TEntity class.</typeparam>
        /// <typeparam name="P">Generic P property class.</typeparam>
        /// <param name="action">Expression of TEntity class for P property</param>
        /// <returns>Entity Attributes object instance.</returns>
        /// <exception cref="NullReferenceException">The property of the expression is null.</exception>
        /// <exception cref="NotSupportedException">The defined LINQ expression is not supported.</exception>
        public static Column CheckExpression<TEntity, P>(Expression<Func<TEntity, P>> action)
        {
            try
            {
                var expression = (MemberExpression)action.Body;
                var attribute = expression.Member.GetCustomAttributes(typeof(Column), true).FirstOrDefault() as Column;
                if(attribute is null)
                    throw new NullReferenceException("The entity attributes definitions in class is null.");
                return attribute;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }
    }
}
