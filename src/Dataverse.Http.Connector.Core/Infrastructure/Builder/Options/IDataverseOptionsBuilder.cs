using System.Reflection;
using Dataverse.Http.Connector.Core.Domains.Builder;
using Dataverse.Http.Connector.Core.Domains.Annotations;
using Dataverse.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dataverse.Http.Connector.Core.Infrastructure.Builder.Options
{
    /// <summary>
    /// This interface defines methods and properties where we will find all the entities configurations to connect to Dataverse.
    /// </summary>
    public interface IDataverseOptionsBuilder
    {
        /// <summary>
        /// Entity builders collection.
        /// </summary>
        ICollection<EntityBuilder> Entities { get; }

        /// <summary>
        /// Function to add multiple entity definitions from an assembly.
        /// </summary>
        /// <param name="assembly">Assembly reference instance.</param>
        void AddEntitiesFromAssembly(Assembly assembly);

        /// <summary>
        /// Function to add multiple entity definitions from an assembly.
        /// </summary>
        /// <param name="type">Class type to get assembly reference instance.</param>
        void AddEntitiesFromAssembly(Type type);

        /// <summary>
        /// Add a new entity builder reference definition.
        /// </summary>
        /// <typeparam name="TEntity">Entity class reference.</typeparam>
        void AddEntityDefinition<TEntity>() where TEntity : class, new();

        /// <summary>
        /// Function to retrive an entity attributes from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entity attribute instance.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public Entity GetEntityAttributesFromType(Type type);

        /// <summary>
        /// Function to retrive an entity column attributes collection from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entitiy columns attributes collection.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public ICollection<Column> GetColumnsAttributesFromType(Type type);
    }
}
