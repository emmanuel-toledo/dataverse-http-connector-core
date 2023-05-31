using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class Annotations
    {
        internal static EntityAttributes? GetEntityAttributes(Type type)
            => type.GetCustomAttributes(typeof(EntityAttributes), true).FirstOrDefault() as EntityAttributes;

        internal static ICollection<FieldAttributes> GetFieldsAttributes(Type type)
        {
            List<FieldAttributes> fields = new();
            var properties = type.GetProperties();
            if (properties is null || properties.Length <= 0)
                return fields;
            foreach (var property in properties)
            {
                if (property.GetCustomAttributes(typeof(FieldAttributes), true).FirstOrDefault() is not FieldAttributes fieldAttributes)
                    continue;
                fields.Add(fieldAttributes);
            }
            return fields;
        }
    }
}
