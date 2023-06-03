using Dynamics.Crm.Http.Connector.Core.Context;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Extensions.Utilities
{
    /// <summary>
    /// This is a class with extension methods to add a new condition in a filter of FetchXml query.
    /// </summary>
    public static class FilterBuilderExtensions
    {
        /// <summary>
        /// Function to add a new condition with 'equal' operator.
        /// <para>
        /// The property's value must be the same with defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Equal<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Equal, value);

        /// <summary>
        /// Function to add a new condition with 'not equal' operator.
        /// <para>
        /// The property's value must be different with defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NotEqual<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotEqual, value);

        /// <summary>
        /// Function to add a new condition with 'null' operator.
        /// <para>
        /// The property's value must be null in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Null<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Null);

        /// <summary>
        /// Function to add a new condition with 'not null' operator.
        /// <para>
        /// The property's value must be not null in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NotNull<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotNull);

        /// <summary>
        /// Function to add a new condition with 'begins with' operator.
        /// <para>
        /// The property's value must begins with defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> BeginsWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.BeginsWith, value);

        /// <summary>
        /// Function to add a new condition with 'does not begin with' operator.
        /// <para>
        /// The property's value must not begins with defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> DoesNotBeginWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.DoesNotBeginWith, value);

        /// <summary>
        /// Function to add a new condition with 'ends with' operator.
        /// <para>
        /// The property's value must ends with defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> EndsWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.EndsWith, value);

        /// <summary>
        /// Function to add a new condition with 'does not end with' operator.
        /// <para>
        /// The property's value must not ends with defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> DoesNotEndsWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.DoesNotEndsWith, value);

        /// <summary>
        /// Function to add a new condition with 'like' operator.
        /// <para>
        /// The property's value must be like (contains) defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Like<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Like, value);

        /// <summary>
        /// Function to add a new condition with 'not like' operator.
        /// <para>
        /// The property's value must not be like (not contains) defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NotLike<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotLike, value);

        /// <summary>
        /// Function to add a new condition with 'in' operator.
        /// <para>
        /// The property's value must be one of the defined values in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Values collection of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> In<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, params P[] value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.In, value);

        /// <summary>
        /// Function to add a new condition with 'not in' operator.
        /// <para>
        /// The property's value must not be one of the defined values in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Values collection of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NotIn<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, params P[] value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotIn, value);

        /// <summary>
        /// Function to add a new condition with 'between' operator.
        /// <para>
        /// The property's value must be between of the defined values in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Values collection of two elements of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        /// <exception cref="ArgumentException">Values must be only two.</exception>
        public static FilterBuilder<TEntity> Between<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, params P[] value) where TEntity : class, new()
        {
            if(value.Length > 2)
                throw new ArgumentException("This operator only supports two values for the array params.");
            return filterBuilder.AddCondition(action, ConditionTypes.Between, value);
        }

        /// <summary>
        /// Function to add a new condition with 'not between' operator.
        /// <para>
        /// The property's value must be not between of the defined values in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Values collection of two elements of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        /// <exception cref="ArgumentException">Values must be only two.</exception>
        public static FilterBuilder<TEntity> NotBetween<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, params P[] value) where TEntity : class, new()
        {
            if (value.Length > 2)
                throw new ArgumentException("This operator only supports two values for the array params.");
            return filterBuilder.AddCondition(action, ConditionTypes.NotBetween, value);
        }
    }
}
