using System.Linq.Expressions;
using Dynamics.Crm.Http.Connector.Core.Utilities;
using Dynamics.Crm.Http.Connector.Core.Domains.Xml;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Builder
{
    /// <summary>
    /// Function to configure the a filter query for a Dynamics request.
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
        public FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type)
        {
            var attribute = FilterBuilderUtilities.CheckExpression(action);
            _filter.Conditions.Add(new Condition(attribute.LogicalName, type, null));
            return this;
        }

        /// <summary>
        /// Function to add a new condition using the deffinition of an attribute using "string" type.
        /// </summary>
        /// <param name="attribute">Attribute to evaluate.</param>
        /// <param name="type">Condition type.</param>
        /// <returns>Same instance of Filter builder.</returns>
        public FilterBuilder<TEntity> AddCondition(string attribute, ConditionTypes type)
        {
            _filter.Conditions.Add(new Condition(attribute, type, null));
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
        public FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type, P value)
        {
            var attribute = FilterBuilderUtilities.CheckExpression(action);
            _filter.Conditions.Add(new Condition(attribute.LogicalName, type, Parse.ParseValue(value)));
            return this;
        }

        /// <summary>
        /// Function to add a new condition using the deffinition of an attribute using "string" type.
        /// </summary>
        /// <typeparam name="P">Property type of a class.</typeparam>
        /// <param name="attribute">Attribute to evaluate.</param>
        /// <param name="type">Condition type.</param>
        /// <param name="value">Value to compare of 'P' type.</param>
        /// <returns>Same instance of Filter builder.</returns>
        public FilterBuilder<TEntity> AddCondition<P>(string attribute, ConditionTypes type, P value)
        {
            _filter.Conditions.Add(new Condition(attribute, type, Parse.ParseValue(value)));
            return this;
        }
    }
}
