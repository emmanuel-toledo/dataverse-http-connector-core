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
        /// Add a new entity builder reference deffinition.
        /// </summary>
        /// <typeparam name="TEntity">Entity class reference.</typeparam>
        void AddEntityDeffinition<TEntity>() where TEntity : class, new();

        /// <summary>
        /// Function to retrive an entity attributes from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entity attribute instance.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public EntityAttributes GetEntityAttributesFromType(Type type);

        /// <summary>
        /// Function to retrive an entity field attributes collection from a specific entity in the Dataverse Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entitiy fields attributes collection.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public ICollection<FieldAttributes> GetFieldsAttributesFromType(Type type);
    }
}
