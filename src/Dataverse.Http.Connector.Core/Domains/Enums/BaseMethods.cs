namespace Dataverse.Http.Connector.Core.Domains.Enums
{
    /// <summary>
    /// This enum defines the operations that can be requested.
    /// </summary>
    public enum BaseMethodTypes
    {
        FirstAsync, // First record or throw exception.
        FirstOrDefaultAsync, // First or default record (null).
        ToListAsync, // Collection of entity records.
        ToPagedListAsync, // Paged list collection of entity records.
        CountAsync, // Count entity records.
        AddAsync, // Add a new record.
        UpdateAsync, // Update a specific record.
        DeleteAsync // Delete a specific record.
    }

    /// <summary>
    /// This class defines the functions to work with enum "BaseMethodTypes".
    /// </summary>
    internal static class BaseMethods
    {
        /// <summary>
        /// Function to parse Base Method Type enum record to Http Method.
        /// </summary>
        /// <param name="method">Base method type enum record.</param>
        /// <returns>Http method type.</returns>
        /// <exception cref="NullReferenceException">Any base method was not selected.</exception>
        internal static HttpMethod ParseToHttpMethod(BaseMethodTypes method)
        {
            return method switch
            {
                BaseMethodTypes.FirstAsync => HttpMethod.Get,
                BaseMethodTypes.FirstOrDefaultAsync => HttpMethod.Get,
                BaseMethodTypes.ToListAsync => HttpMethod.Get,
                BaseMethodTypes.ToPagedListAsync => HttpMethod.Get,
                BaseMethodTypes.CountAsync => HttpMethod.Get,
                BaseMethodTypes.AddAsync => HttpMethod.Post,
                BaseMethodTypes.UpdateAsync => HttpMethod.Patch,
                BaseMethodTypes.DeleteAsync => HttpMethod.Delete,
                _ => throw new NullReferenceException("Any base method type was not selected."),
            };
        }
    }
}
