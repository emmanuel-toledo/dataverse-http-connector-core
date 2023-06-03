using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;

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

        /// <summary>
        /// Function to retrive an entity attributes from a specific entity in the Dynamics Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entity attribute instance.</returns>
        /// <exception cref="NotDefinitionEntityException">The entity type was not found in the context.</exception>
        public EntityAttributes GetEntityAttributesFromType(Type type)
        {
            if(!Entities.Any(x => x.EntityType == type))
                throw new NotDefinitionEntityException(type.Name, type);
            return Entities.First(x => x.EntityType == type).EntityAttributes!;
        }

        /// <summary>
        /// Function to retrive an entity field attributes collection from a specific entity in the Dynamics Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entitiy fields attributes collection.</returns>
        /// <exception cref="NotDefinitionEntityException">The entity type was not found in the context.</exception>
        public ICollection<FieldAttributes> GetFieldsAttributesFromType(Type type)
        {
            if (!Entities.Any(x => x.EntityType == type))
                throw new NotDefinitionEntityException(type.Name, type);
            return Entities.First(x => x.EntityType == type).FieldsAttributes;
        }
    }
}
