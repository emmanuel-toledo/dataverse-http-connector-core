using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Exceptions;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options
{
    /// <summary>
    /// This interface defines methods and properties where we will find all the entities configurations to connect to Dynamics.
    /// </summary>
    public interface IDynamicsOptionsBuilder
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
        /// Function to retrive an entity attributes from a specific entity in the Dynamics Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entity attribute instance.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public EntityAttributes GetEntityAttributesFromType(Type type);

        /// <summary>
        /// Function to retrive an entity field attributes collection from a specific entity in the Dynamics Option Builder entities collection.
        /// </summary>
        /// <param name="type">Type of entity to retrive.</param>
        /// <returns>Entitiy fields attributes collection.</returns>
        /// <exception cref="EntityDefinitionException">The entity type was not found in the context.</exception>
        public ICollection<FieldAttributes> GetFieldsAttributesFromType(Type type);
    }
}
