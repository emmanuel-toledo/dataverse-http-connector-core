using System.Linq.Expressions;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    /// <summary>
    /// This class utility functions to check if the information of the "FilterBuilder" class is correctly.
    /// </summary>
    internal static class FilterBuilderCheck
    {
        /// <summary>
        /// Check if the expression is correctly created and configured to get "EntityAttributes" custom attribute.
        /// </summary>
        /// <typeparam name="TEntity">Generic TEntity class.</typeparam>
        /// <typeparam name="P">Generic P property class.</typeparam>
        /// <param name="action">Expression of TEntity class for P property</param>
        /// <returns>Entity Attributes object instance.</returns>
        /// <exception cref="NullReferenceException">The property of the expression is null.</exception>
        /// <exception cref="NotSupportedException">The defined LINQ expression is not supported.</exception>
        public static EntityAttributes CheckExpression<TEntity, P>(Expression<Func<TEntity, P>> action)
        {
            try
            {
                var expression = (MemberExpression)action.Body;
                var attribute = expression.Member.GetCustomAttributes(typeof(EntityAttributes), true).FirstOrDefault() as EntityAttributes ?? throw new NullReferenceException("The entity attributes definitions in class is null.");
                return attribute;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }
    }
}
