using Dynamics.Crm.Http.Connector.Core.Models.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.Business.Infrastructure.Annotations
{
    /// <summary>
    /// This class works to define entity's field information in a custom class attribute.
    /// </summary>
    public class FieldAttributes : Attribute
    {
        /// <summary>
        /// Get and set field Schema name.
        /// </summary>
        public string? SchemaName { get; set; } = string.Empty;

        /// <summary>
        /// Get an set field logical name.
        /// </summary>
        public string? LogicalName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set field type.
        /// </summary>
        public FieldTypes FieldType { get; set; } = FieldTypes.Text;
    }
}
