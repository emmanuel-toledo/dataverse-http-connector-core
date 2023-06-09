using System.Linq.Expressions;
using Dataverse.Http.Connector.Core.Utilities;
using Dataverse.Http.Connector.Core.Domains.Xml;
using Dataverse.Http.Connector.Core.Domains.Enums;

namespace Dataverse.Http.Connector.Core.Domains.Builder
{
    /// <summary>
    /// Function to configure the a filter query for a Dataverse request.
    /// </summary>
    /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
    public class FilterBuilder<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Private filter object to define "filter" tag in query.
        /// </summary>
        private Filter _filter { get; set; }

        /// <summary>
        /// Get the selected filter type (and, or).
        /// </summary>
        public FilterTypes FilterType { get => _filter.FilterType; }

        /// <summary>
        /// Get the collection of conditions that was defined.
        /// </summary>
        public ICollection<Condition> Conditions { get => _filter.Conditions; }

        /// <summary>
        /// Initialzie a new instance of "Filter Builder" class.
        /// </summary>
        /// <param name="type">Filter type.</param>
        public FilterBuilder(FilterTypes type) 
            => _filter = new(type);

        /// <summary>
        /// Function to add a new condition using an expression to select the property of a class.
        /// </summary>
        /// <typeparam name="P">Property type of a class.</typeparam>
        /// <param name="action">Expression of custom class.</param>
        /// <param name="type">Condition type.</param>
        /// <returns>Same instance of Filter builder.</returns>
        internal FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type)
        {
            var attribute = FilterBuilderUtilities.CheckExpression(action);
            _filter.Conditions.Add(new Condition(attribute.LogicalName, type, null));
            return this;
        }

        /// <summary>
        /// Function to add a new condition using an expression to select the property of a class.
        /// </summary>
        /// <typeparam name="P">Property type of a class.</typeparam>
        /// <param name="action">Expression of custom class.</param>
        /// <param name="type">Condition type.</param>
        /// <param name="value">Value to compare of 'P' type.</param>
        /// <returns>Same instance of Filter builder.</returns>
        internal FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type, P value)
        {
            var attribute = FilterBuilderUtilities.CheckExpression(action);
            _filter.Conditions.Add(new Condition(attribute.LogicalName, type, Parse.ParseValue(value)));
            return this;
        }

        /// <summary>
        /// Function to add a new condition using an expression to select the property of a class and add multiples values.
        /// </summary>
        /// <typeparam name="P">Property type of a class.</typeparam>
        /// <param name="action">Expression of custom class.</param>
        /// <param name="type">Condition type.</param>
        /// <param name="value">Value to compare of 'P' type.</param>
        /// <returns>Same instance of Filter builder.</returns>
        internal FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type, P[] value)
        {
            var attribute = FilterBuilderUtilities.CheckExpression(action);
            _filter.Conditions.Add(new Condition(attribute.LogicalName, type, value));
            return this;
        }
    }
}
