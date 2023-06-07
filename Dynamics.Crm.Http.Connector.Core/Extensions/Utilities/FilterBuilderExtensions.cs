using System.Linq.Expressions;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;

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

        /// <summary>
        /// Function to add a new condition with 'greater than' operator.
        /// <para>
        /// The property's value must be greater than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> GreaterThan<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.GreaterThan, value);

        /// <summary>
        /// Function to add a new condition with 'greater or equal' operator.
        /// <para>
        /// The property's value must be greater or equal than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> GreaterEqual<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.GreaterEqual, value);

        /// <summary>
        /// Function to add a new condition with 'less than' operator.
        /// <para>
        /// The property's value must be less than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LessThan<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LessThan, value);

        /// <summary>
        /// Function to add a new condition with 'less or equal' operator.
        /// <para>
        /// The property's value must be less or equal than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LessEqual<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LessEqual, value);

        /// <summary>
        /// Function to add a new condition with 'last 7 days' operator.
        /// <para>
        /// The property's value must be inside of the last seven days than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Last7Days<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Last7Days);

        /// <summary>
        /// Function to add a new condition with 'last fiscal period' operator.
        /// <para>
        /// The property's value must be inside of the last fiscal period than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LastFiscalPeriod<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LastFiscalPeriod);

        /// <summary>
        /// Function to add a new condition with 'last fiscal year' operator.
        /// <para>
        /// The property's value must be inside of the last fiscal year than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LastFiscalYear<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LastFiscalYear);

        /// <summary>
        /// Function to add a new condition with 'last month' operator.
        /// <para>
        /// The property's value must be inside of the last month than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LastMonth<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LastMonth);

        /// <summary>
        /// Function to add a new condition with 'last week' operator.
        /// <para>
        /// The property's value must be inside of the last week than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LastWeek<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LastWeek);

        /// <summary>
        /// Function to add a new condition with 'last year' operator.
        /// <para>
        /// The property's value must be inside of the last year than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> LastYear<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.LastYear);

        /// <summary>
        /// Function to add a new condition with 'next 7 days' operator.
        /// <para>
        /// The property's value must be inside of the next seven days than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Next7Days<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Next7Days);

        /// <summary>
        /// Function to add a new condition with 'next fiscal period' operator.
        /// <para>
        /// The property's value must be inside of the next fiscal period than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NextFiscalPeriod<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NextFiscalPeriod);

        /// <summary>
        /// Function to add a new condition with 'next fiscal year' operator.
        /// <para>
        /// The property's value must be inside of the next fiscal year than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NextFiscalYear<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NextFiscalYear);

        /// <summary>
        /// Function to add a new condition with 'next month' operator.
        /// <para>
        /// The property's value must be inside of the next month than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NextMonth<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NextMonth);

        /// <summary>
        /// Function to add a new condition with 'next week' operator.
        /// <para>
        /// The property's value must be inside of the next week than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NextWeek<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NextWeek);

        /// <summary>
        /// Function to add a new condition with 'next year' operator.
        /// <para>
        /// The property's value must be inside of the next year than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> NextYear<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NextYear);

        /// <summary>
        /// Function to add a new condition with 'this fiscal period' operator.
        /// <para>
        /// The property's value must be inside of the current fiscal period than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> ThisFiscalPeriod<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.ThisFiscalPeriod);

        /// <summary>
        /// Function to add a new condition with 'this fiscal year' operator.
        /// <para>
        /// The property's value must be inside of the current fiscal year than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> ThisFiscalYear<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.ThisFiscalYear);

        /// <summary>
        /// Function to add a new condition with 'this month' operator.
        /// <para>
        /// The property's value must be inside of the current month than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> ThisMonth<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.ThisMonth);

        /// <summary>
        /// Function to add a new condition with 'this week' operator.
        /// <para>
        /// The property's value must be inside of the current week than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> ThisWeek<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.ThisWeek);

        /// <summary>
        /// Function to add a new condition with 'this year' operator.
        /// <para>
        /// The property's value must be inside of the current year than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> ThisYear<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.ThisYear);

        /// <summary>
        /// Function to add a new condition with 'today' operator.
        /// <para>
        /// The property's value must be inside of the current day than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Today<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Today);

        /// <summary>
        /// Function to add a new condition with 'tomorrow' operator.
        /// <para>
        /// The property's value must be inside of the day of tomorro than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Tomorrow<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Tomorrow);

        /// <summary>
        /// Function to add a new condition with 'yesterday' operator.
        /// <para>
        /// The property's value must be inside of the day of yesterday than the defined value in Dataverse record.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">TEntity class type.</typeparam>
        /// <typeparam name="P">TEntity's property reference.</typeparam>
        /// <param name="filterBuilder">Instance of filter builder.</param>
        /// <param name="action">TEntity property reference.</param>
        /// <param name="value">Single value of TEntity's property.</param>
        /// <returns>Same instance of filter builder.</returns>
        public static FilterBuilder<TEntity> Yesterday<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Yesterday);
    }
}
