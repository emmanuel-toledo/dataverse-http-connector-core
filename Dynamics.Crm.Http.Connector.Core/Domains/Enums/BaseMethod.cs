namespace Dynamics.Crm.Http.Connector.Core.Domains.Enums
{
    /// <summary>
    /// This enum defines the operations that can be requested.
    /// </summary>
    public enum BaseMethod
    {
        None, // Not selected method.
        FirstOrDefaultAsync, // First or default record.
        ToListAsync, // Collection of entity records.
        AddAsync, // Add a new record.
        UpdateAzync, // Update a specific record.
        DeleteAzync // Delete a specific record.
    }
}
