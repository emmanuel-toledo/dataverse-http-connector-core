﻿namespace Dataverse.Http.Connector.Core.Domains.Annotations
{
    /// <summary>
    /// This class works to define entity's field information in a custom class attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Field : Attribute
    {
        /// <summary>
        /// Creates a new instance of Field Attributes with properties used to configure Dataverse requests.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="fieldType">Property field type.</param>
        public Field(string schemaName, string logicalName, FieldTypes fieldType)
        {
            SchemaName = schemaName;
            LogicalName = logicalName;
            FieldType = fieldType;
        }

        /// <summary>
        /// Creates a new instance of Field Attributes with properties used to configure Dataverse requests.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="fieldType">Property field type.</param>
        /// <param name="linkedEntityLogicalCollectionName">Related entity logical collection name.</param>
        public Field(string schemaName, string logicalName, FieldTypes fieldType, string linkedEntityLogicalCollectionName)
        {
            SchemaName = schemaName;
            LogicalName = logicalName;
            FieldType = fieldType;
            LinkedEntityLogicalCollectionName = linkedEntityLogicalCollectionName;
        }

        /// <summary>
        /// Get and set the property name defined inside the TEntity class.
        /// </summary>
        internal string TEntityPropertyName { get; set; } = string.Empty;

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

        /// <summary>
        /// Get an set linked entity logical collection name.
        /// </summary>
        public string? LinkedEntityLogicalCollectionName { get; set; } = string.Empty;
    }
}
