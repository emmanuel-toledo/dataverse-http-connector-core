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

namespace Dynamics.Crm.Http.Connector.Core.Extensions.Builders
{
    public static class FilterBuilderExtensions
    {
        public static FilterBuilder<TEntity> Equal<TEntity, P>(this FilterBuilder<TEntity> filterBuilder, 
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Equal, value);

        public static FilterBuilder<TEntity> NotEqual<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotEqual, value);

        public static FilterBuilder<TEntity> Null<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Null);

        public static FilterBuilder<TEntity> NotNull<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotNull);

        public static FilterBuilder<TEntity> BeginsWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.BeginsWith, value);

        public static FilterBuilder<TEntity> DoesNotBeginWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.DoesNotBeginWith, value);

        public static FilterBuilder<TEntity> EndsWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.EndsWith, value);

        public static FilterBuilder<TEntity> DoesNotEndsWith<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.DoesNotEndsWith, value);

        public static FilterBuilder<TEntity> Like<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.Like, value);

        public static FilterBuilder<TEntity> NotLike<TEntity, P>(this FilterBuilder<TEntity> filterBuilder,
            Expression<Func<TEntity, P>> action, P value) where TEntity : class, new()
            => filterBuilder.AddCondition(action, ConditionTypes.NotLike, value);
    }
}
