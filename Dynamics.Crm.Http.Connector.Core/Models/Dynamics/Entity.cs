namespace Dynamics.Crm.Http.Connector.Core.Models.Dynamics
{
    /// <summary>
    /// This class contains each property of the deffinition of an Entity in Dynamics CRM.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Get and set Entity unique identifier.
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Get and set Entity overwrite time.
        /// </summary>
        public DateTime OverwriteTime { get; set; }

        /// <summary>
        /// Get and set Entity is activity flag. 
        /// </summary>
        public bool IsActivity { get; set; }

        /// <summary>
        /// Get and set solution unique identifier.
        /// </summary>
        public Guid SolutionId { get; set; }

        /// <summary>
        /// Get and set Entity original localized collection name. 
        /// </summary>
        public string? OriginalLocalizedCollectionName { get; set; }

        /// <summary>
        /// Get and set Entity collection name. 
        /// </summary>
        public string? CollectionName { get; set; }

        /// <summary>
        /// Get and set Entity original localized name.
        /// </summary>
        public string? OriginalLocalizedName { get; set; }

        /// <summary>
        /// Get and set Entity object type code. 
        /// </summary>
        public double ObjectTypeCode { get; set; }

        /// <summary>
        /// Get and set Entity logical collection name. 
        /// </summary>
        public string? LogicalCollectionName { get; set; }

        /// <summary>
        /// Get and set Entity component state.
        /// </summary>
        public int ComponentState { get; set; }

        /// <summary>
        /// Get and set Entity set name. 
        /// </summary>
        public string? EntitySetName { get; set; }

        /// <summary>
        /// Get and set Entity version number.
        /// </summary>
        public double VersionNumber { get; set; }

        /// <summary>
        /// Get and set Entity logical name. 
        /// </summary>
        public string? LogicalName { get; set; }

        /// <summary>
        /// Get and set Entity physical name.
        /// </summary>
        public string? PhysicalName { get; set; }

        /// <summary>
        /// Get and set Entity base table name.
        /// </summary>
        public string? BaseTableName { get; set; }

        /// <summary>
        /// Get and set Entity report view name.
        /// </summary>
        public string? ReportViewName { get; set; }

        /// <summary>
        /// Get and set Entity name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Get and set Entity address table name.
        /// </summary>
        public string? AddressTableName { get; set; }

        /// <summary>
        /// Get and set Entity extension table name. 
        /// </summary>
        public string? ExtensionTableName { get; set; }

        /// <summary>
        /// Get and set Entity parents controlling attribute name.
        /// </summary>
        public string? ParentsControllingAttributeName { get; set; }

        /// <summary>
        /// Get and set Entity external name.
        /// </summary>
        public string? ExternalName { get; set; }

        /// <summary>
        /// Get and set Entity external collection name.
        /// </summary>
        public string? ExternalCollectionName { get; set; }
    }
}
