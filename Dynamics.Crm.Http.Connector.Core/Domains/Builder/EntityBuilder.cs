using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Builder
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
        private readonly EntityAttributes? _entityAttributes;

        /// <summary>
        /// List of entity fields attributes definitions from class.
        /// </summary>
        private readonly ICollection<FieldAttributes> _fieldsAttributes;

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
            // Get entity fields annotation attributes.
            _fieldsAttributes = GetFieldsAttributes();
        }

        /// <summary>
        /// Get Entity type value.
        /// </summary>
        public Type EntityType { get => _entityType; }

        /// <summary>
        /// Get Entity Attributes scheme.
        /// </summary>
        public EntityAttributes? EntityAttributes { get => _entityAttributes; }

        /// <summary>
        /// Get Fields Attributes collection.
        /// </summary>
        public ICollection<FieldAttributes> FieldsAttributes { get => _fieldsAttributes ?? new HashSet<FieldAttributes>(); }

        /// <summary>
        /// Function to extract in a model the Entity Attributes from a class.
        /// </summary>
        /// <returns>Entity attributes instance.</returns>
        /// <exception cref="NullReferenceException">The class does not use "EntityAttributes".</exception>
        private EntityAttributes GetEntityAttributes()
            => _entityType.GetCustomAttributes(typeof(EntityAttributes), true).FirstOrDefault() as EntityAttributes ??
               throw new NullReferenceException($"The entity attributes definitions in class {_entityType.Name} is null.");

        /// <summary>
        /// Function to extract in a collection the fields attributes of an entity from a class.
        /// </summary>
        /// <returns>Field attributes collection.</returns>
        private ICollection<FieldAttributes> GetFieldsAttributes()
        {
            List<FieldAttributes> fields = new();
            var properties = _entityType.GetProperties();
            if (properties is null || properties.Length <= 0)
                return fields;
            foreach (var property in properties)
            {
                if (property.GetCustomAttributes(typeof(FieldAttributes), true).FirstOrDefault() is not FieldAttributes fieldAttributes)
                    continue;
                fieldAttributes.TEntityPropertyName = property.Name;
                fields.Add(fieldAttributes);
            }
            return fields;
        }
    }
}
