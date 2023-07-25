using Dataverse.Http.Connector.Core.Domains.Annotations;

namespace Dataverse.Http.Connector.Core.Domains.Builder
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
        /// Entity attributes definitions from class.
        /// </summary>
        private readonly Entity? _entityAttributes;

        /// <summary>
        /// List of entity columns attributes definitions from class.
        /// </summary>
        private readonly ICollection<Column> _columnsAttributes;

        /// <summary>
        /// Initialize a new Entity Builder object.
        /// </summary>
        /// <param name="entityType">Entity type model.</param>
        /// <exception cref="ArgumentNullException">Type reference is null.</exception>
        public EntityBuilder(Type entityType)
        {
            // Validate null value.
            if (entityType is null) throw new ArgumentNullException(nameof(entityType));
            // Set private readonly properties.
            _entityType = entityType;
            // Get entity annotation attributes.
            _entityAttributes = GetEntityAttributes();
            // Get entity columns annotation attributes.
            _columnsAttributes = GetColumnsAttributes();
        }

        /// <summary>
        /// Get Entity type value.
        /// </summary>
        public Type EntityType { get => _entityType; }

        /// <summary>
        /// Get Entity Attributes scheme.
        /// </summary>
        public Entity? EntityAttributes { get => _entityAttributes; }

        /// <summary>
        /// Get Columns Attributes collection.
        /// </summary>
        public ICollection<Column> ColumnsAttributes { get => _columnsAttributes ?? new HashSet<Column>(); }

        /// <summary>
        /// Function to extract in a model the Entity Attributes from a class.
        /// </summary>
        /// <returns>Entity attributes instance.</returns>
        /// <exception cref="NullReferenceException">The class does not use "Entity" attributes.</exception>
        private Entity GetEntityAttributes()
            => _entityType.GetCustomAttributes(typeof(Entity), true).FirstOrDefault() as Entity ??
               throw new NullReferenceException($"The entity attributes definitions in class {_entityType.Name} is null.");

        /// <summary>
        /// Function to extract in a collection the columns attributes of an entity from a class.
        /// </summary>
        /// <returns>Column attributes collection.</returns>
        private ICollection<Column> GetColumnsAttributes()
        {
            List<Column> columns = new();
            var properties = _entityType.GetProperties();
            if (properties is null || properties.Length <= 0)
                return columns;
            foreach (var property in properties)
            {
                if (property.GetCustomAttributes(typeof(Column), true).FirstOrDefault() is not Column columnAttributes)
                    continue;
                columnAttributes.TEntityPropertyName = property.Name;
                columns.Add(columnAttributes);
            }
            return columns;
        }
    }
}
