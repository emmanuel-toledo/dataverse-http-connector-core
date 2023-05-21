namespace Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Annotations
{
    /// <summary>
    /// This class works to define entity information in a custom class.
    /// </summary>
    public class EntityAttributes : Attribute
    {
        public EntityAttributes(string schemaName, string logicalName)
        {
            SchemaName = schemaName;
            LogicalName = logicalName;
        }

        /// <summary>
        /// Get and set schema name of an entity.
        /// </summary>
        public string? SchemaName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set logical name of an entity.
        /// </summary>
        public string? LogicalName { get; set; } = string.Empty;
    }
}
