using Dataverse.Http.Connector.Core.Domains.Builder;
using Dataverse.Http.Connector.Core.Domains.Annotations;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dataverse.Http.Connector.Core.Infrastructure.Builder.Options
{
    /// <summary>
    /// This class implements methods and properties where we will find all the entities configurations to connect to Dataverse.
    /// </summary>
    public class DataverseOptionsBuilder : IDataverseOptionsBuilder
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
        /// Add a new entity builder reference definition.
        /// </summary>
        /// <typeparam name="TEntity">Entity class reference.</typeparam>
        /// <exception cref="ApplicationBuilderException">Application builder exception.</exception>
        public void AddEntityDefinition<TEntity>() where TEntity : class, new()
        {
            if (Entities.Any(x => x.EntityType == typeof(TEntity)))
                throw new ApplicationBuilderException($"The entity type '{ typeof(TEntity) }' is already configured.");
            _entities.Add(new EntityBuilder(typeof(TEntity)));
        }

        /// <summary>
        /// Function to retrive an entity attributes from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entity attribute instance.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public EntityAttributes GetEntityAttributesFromType(Type type)
        {
            if(!Entities.Any(x => x.EntityType == type))
                throw new EntityDefinitionException(type.Name, type);
            return Entities.First(x => x.EntityType == type).EntityAttributes!;
        }

        /// <summary>
        /// Function to retrive an entity field attributes collection from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entitiy fields attributes collection.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public ICollection<FieldAttributes> GetFieldsAttributesFromType(Type type)
        {
            if (!Entities.Any(x => x.EntityType == type))
                throw new EntityDefinitionException(type.Name, type);
            return Entities.First(x => x.EntityType == type).FieldsAttributes;
        }
    }
}
