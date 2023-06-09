﻿namespace Dataverse.Http.Connector.Core.Domains.Annotations
{
    /// <summary>
    /// This class works to define entity information in a custom class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Entity : Attribute
    {
        /// <summary>
        /// Creates a new instance of Entity Attributes with properties used to configure Dataverse requests.
        /// </summary>
        /// <param name="logicalName">Entity logical name.</param>
        /// <param name="logicalCollectionName">Entity logical collection name.</param>
        public Entity(string logicalName, string logicalCollectionName)
        {
            LogicalName = logicalName;
            LogicalCollectionName = logicalCollectionName;
        }

        /// <summary>
        /// Get and set logical name of an entity.
        /// </summary>
        public string? LogicalCollectionName { get; set; } = string.Empty;

        /// <summary>
        /// Get and set logical collection name of an entity.
        /// </summary>
        public string? LogicalName { get; set; } = string.Empty;
    }
}
