using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options
{
    /// <summary>
    /// This class implements methods and properties where we will find all the entities configurations to connect to Dynamics.
    /// </summary>
    public class DynamicsOptionsBuilder : IDynamicsOptionsBuilder
    {
        /// <summary>
        /// Get and set entity builders collection.
        /// </summary>
        private ICollection<EntityBuilder> _entities { get; set; } = new HashSet<EntityBuilder>();

        /// <summary>
        /// Get entity builders collection.
        /// </summary>
        public ICollection<EntityBuilder> Entities { get => _entities; }

        /// <summary>
        /// Add a new entity builder reference deffinition.
        /// </summary>
        /// <typeparam name="TEntity">Entity class reference.</typeparam>
        /// <exception cref="ApplicationBuilderException">Application builder exception.</exception>
        public void AddEntityDeffinition<TEntity>() where TEntity : class, new()
        {
            if (Entities.Any(x => x.EntityType == typeof(TEntity)))
                throw new ApplicationBuilderException($"The entity type '{ typeof(TEntity) }' is already configured.");
            _entities.Add(new EntityBuilder(typeof(TEntity)));
        }
    }
}
