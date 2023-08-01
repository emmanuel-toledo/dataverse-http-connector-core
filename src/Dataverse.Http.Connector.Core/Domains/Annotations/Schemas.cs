namespace Dataverse.Http.Connector.Core.Domains.Annotations
{
    /// <summary>
    /// Attribute to define a "Text" Column for an Entity.
    /// </summary>
    public class Text : Column
    {
        /// <summary>
        /// Create a new instance of "Text" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public Text(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.Text, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "Number" Column for an Entity.
    /// </summary>
    public class Number : Column
    {
        /// <summary>
        /// Create a new instance of "Number" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public Number(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.Number, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "DecimalNumber" Column for an Entity.
    /// </summary>
    public class DecimalNumber : Column
    {
        /// <summary>
        /// Create a new instance of "DecimalNumber" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public DecimalNumber(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.DecimalNumber, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "Lookup" Column for an Entity.
    /// </summary>
    public class Lookup : Column
    {
        /// <summary>
        /// Create a new instance of "Lookup" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="linkedEntityLogicalCollectionName">Related entity logical collection name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public Lookup(string schemaName, string logicalName, string linkedEntityLogicalCollectionName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.Lookup, linkedEntityLogicalCollectionName, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "DateTimeOf" Column for an Entity.
    /// </summary>
    public class DateTimeOf : Column
    {
        /// <summary>
        /// Create a new instance of "DateTimeOf" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public DateTimeOf(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.DateTime, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "OptionSet" Column for an Entity.
    /// </summary>
    public class OptionSet : Column
    {
        /// <summary>
        /// Create a new instance of "OptionSet" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public OptionSet(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.OptionSet, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "BoolOptionSet" Column for an Entity.
    /// </summary>
    public class BoolOptionSet : Column
    {
        /// <summary>
        /// Create a new instance of "BoolOptionSet" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public BoolOptionSet(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.BoolOptionSet, readOnly)
        { }
    }

    /// <summary>
    /// Attribute to define a "UniqueIdentifier" Column for an Entity.
    /// </summary>
    public class UniqueIdentifier : Column
    {
        /// <summary>
        /// Create a new instance of "UniqueIdentifier" Column attribute.
        /// </summary>
        /// <param name="schemaName">Property schema name.</param>
        /// <param name="logicalName">Property logical name.</param>
        /// <param name="readOnly">Flag to identify if this column must be used for queries only and not for create or update operations.</param>
        public UniqueIdentifier(string schemaName, string logicalName, bool readOnly = false) : base(schemaName, logicalName, ColumnTypes.UniqueIdentifier, readOnly)
        { }
    }
}
