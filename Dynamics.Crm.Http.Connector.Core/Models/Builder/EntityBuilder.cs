namespace Dynamics.Crm.Http.Connector.Core.Models.Builder
{
    /// <summary>
    /// This class works to define an Entity model using the instance of a class.
    /// </summary>
    public class EntityBuilder
    {
        /// <summary>
        /// Entity type value.
        /// </summary>
        private readonly Type _entityType;

        /// <summary>
        /// Initialize a new Entity Builder object.
        /// </summary>
        /// <param name="entityType">Entity type model.</param>
        /// <exception cref="ArgumentNullException">Type reference is null.</exception>
        public EntityBuilder(Type entityType)
        {
            if (entityType is null)
                throw new ArgumentNullException(nameof(entityType));
            _entityType = entityType;
        }

        /// <summary>
        /// Get Entity type value.
        /// </summary>
        public Type EntityType { get => _entityType; }
    }
}
