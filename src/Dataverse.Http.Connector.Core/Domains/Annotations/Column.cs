namespace Dataverse.Http.Connector.Core.Domains.Annotations
{
    /// <summary>
    /// This class works to define entity's column information in a custom class attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Column : Attribute
    {
        /// <summary>
        /// Creates a new instance of column attribute with properties used to configure Dataverse requests.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="columnType">Property column type.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public Column(string schemaName, string logicalName, ColumnTypes columnType, bool readOnly = false)
        {
            SchemaName = schemaName;
            LogicalName = logicalName;
            ColumnType = columnType;
        }

        /// <summary>
        /// Creates a new instance of column attribute with properties used to configure Dataverse requests.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="columnType">Property column type.</param>
        /// <param name="linkedEntityLogicalCollectionName">Related entity logical collection name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public Column(string schemaName, string logicalName, ColumnTypes columnType, string linkedEntityLogicalCollectionName, bool readOnly = false)
        {
            SchemaName = schemaName;
            LogicalName = logicalName;
            ColumnType = columnType;
            LinkedEntityLogicalCollectionName = linkedEntityLogicalCollectionName;
        }

        /// <summary>
        /// Get and set the property name defined inside the TEntity class.
        /// </summary>
        internal string TEntityPropertyName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set column Schema name.
        /// </summary>
        public string? SchemaName { get; set; } = string.Empty;

        /// <summary>
        /// Get an set column logical name.
        /// </summary>
        public string? LogicalName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set column type.
        /// </summary>
        public ColumnTypes ColumnType { get; set; } = ColumnTypes.Text;

        /// <summary>
        /// Get an set linked entity logical collection name.
        /// </summary>
        public string? LinkedEntityLogicalCollectionName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set readonly entity column.
        /// </summary>
        public bool ReadOnly { get; set; } = false;
    }
}
