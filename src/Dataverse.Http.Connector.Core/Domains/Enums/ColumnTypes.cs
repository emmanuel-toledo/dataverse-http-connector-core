﻿namespace Dataverse.Http.Connector.Core.Domains.Annotations
{
    /// <summary>
    /// This enum works to define the Column Type of an Entity attribute in Dataverse (Dynamics CRM).
    /// </summary>
    public enum ColumnTypes
    {
        Text,
        Number,
        DecimalNumber,
        Lookup,
        DateTime,
        OptionSet,
        BoolOptionSet,
        UniqueIdentifier
    }
}
