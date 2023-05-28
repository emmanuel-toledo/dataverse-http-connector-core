using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Utilities;
using System.Linq.Expressions;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Builder
{
    public class FilterBuilder<TEntity> where TEntity : class, new()
    {
        private readonly FilterTypes _type;

        private List<ConditionBuilder> _conditions = new();

        public FilterBuilder(FilterTypes type) 
            => _type = type;

        public FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type)
        {
            var attribute = FilterBuilderCheck.CheckExpression(action);
            _conditions.Add(new ConditionBuilder(attribute.LogicalName, type, null));
            return this;
        }

        public FilterBuilder<TEntity> AddCondition<P>(string attribute, ConditionTypes type)
        {
            _conditions.Add(new ConditionBuilder(attribute, type, null));
            return this;
        }

        public FilterBuilder<TEntity> AddCondition<P>(Expression<Func<TEntity, P>> action, ConditionTypes type, P value)
        {
            var attribute = FilterBuilderCheck.CheckExpression(action);
            _conditions.Add(new ConditionBuilder(attribute.LogicalName, type, Parse.ParseValue(value)));
            return this;
        }

        public FilterBuilder<TEntity> AddCondition<P>(string attribute, ConditionTypes type, P value)
        {
            _conditions.Add(new ConditionBuilder(attribute, type, Parse.ParseValue(value)));
            return this;
        }

        /// <summary>
        /// This function creates a new "filter" tag for a FetchXml query to be used to consume Dynamics service.
        /// <para>
        /// Already includes specified "condition" tags.
        /// </para>
        /// </summary>
        /// <returns>Filter tag in string format.</returns>
        internal string Build()
        {
            string filter = "<filter>";
            foreach (var condition in _conditions)
            {
                filter += condition.Build();
            }
            filter += "</filter>";
            return filter;
        }
    }
}
