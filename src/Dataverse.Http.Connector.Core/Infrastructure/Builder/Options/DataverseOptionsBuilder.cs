using System.Reflection;
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
        /// Function to add multiple entity definitions from an assembly.
        /// </summary>
        /// <param name="assembly">Assembly reference instance.</param>
        public void AddEntitiesFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t.IsDefined(typeof(Entity)));
            foreach (var type in types)
            {
                if (Entities.Any(x => x.EntityType == type))
                    throw new ApplicationBuilderException($"The entity type '{type}' is already configured.");
                _entities.Add(new(type));
            }
        }

        /// <summary>
        /// Function to add multiple entity definitions from an assembly.
        /// </summary>
        /// <param name="type">Class type to get assembly reference instance.</param>
        public void AddEntitiesFromAssembly(Type type)
            => AddEntitiesFromAssembly(type.Assembly);

        /// <summary>
        /// Add a new entity builder reference definition.
        /// </summary>
        /// <typeparam name="TEntity">Entity class reference.</typeparam>
        /// <exception cref="ApplicationBuilderException">Application builder exception.</exception>
        public void AddEntityDefinition<TEntity>() where TEntity : class, new()
        {
            if (Entities.Any(x => x.EntityType == typeof(TEntity)))
                throw new ApplicationBuilderException($"The entity type '{ typeof(TEntity) }' is already configured.");
            _entities.Add(new(typeof(TEntity)));
        }

        /// <summary>
        /// Function to retrive an entity attributes from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entity attribute instance.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public Entity GetEntityAttributesFromType(Type type)
        {
            if(!Entities.Any(x => x.EntityType == type))
                throw new EntityDefinitionException(type.Name, type);
            return Entities.First(x => x.EntityType == type).EntityAttributes!;
        }

        /// <summary>
        /// Function to retrive an entity column attributes collection from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entitiy columns attributes collection.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public ICollection<Column> GetColumnsAttributesFromType(Type type)
        {
            if (!Entities.Any(x => x.EntityType == type))
                throw new EntityDefinitionException(type.Name, type);
            return Entities.First(x => x.EntityType == type).ColumnsAttributes;
        }
    }
}
