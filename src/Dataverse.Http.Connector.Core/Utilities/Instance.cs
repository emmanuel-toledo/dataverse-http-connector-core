using Dataverse.Http.Connector.Core.Context;
using Dataverse.Http.Connector.Core.Domains.Enums;
using Dataverse.Http.Connector.Core.Domains.Builder;

namespace Dataverse.Http.Connector.Core.Utilities
{
    /// <summary>
    /// This is an utility class to generate new instances of different classes.
    /// </summary>
    /// <typeparam name="TEntity">Custom class reference type.</typeparam>
    internal static class Instance<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Function to create a new instance of TEntity class.
        /// </summary>
        /// <returns>TEntity instance.</returns>
        public static TEntity TEntityInstance()
            => (TEntity)Activator.CreateInstance(typeof(TEntity))!;

        /// <summary>
        /// Function to create a new DbEntitySet class instance of type TEntity.
        /// </summary>
        /// <returns>DbEntitySet class instance of type TEntity.</returns>
        public static DbEntitySet<TEntity> DbEntitySetInstance()
            => (DbEntitySet<TEntity>)Activator.CreateInstance(typeof(DbEntitySet<TEntity>))!;

        /// <summary>
        /// Function to create a new instance of FilterBuilder class of type TEntity.
        /// </summary>
        /// <param name="type">Filter builder type.</param>
        /// <returns>New instance of FilterBuilder class of type TEntity</returns>
        public static FilterBuilder<TEntity> FilterBuilderInstance(FilterTypes type)
            => (FilterBuilder<TEntity>)Activator.CreateInstance(typeof(FilterBuilder<TEntity>), type)!;
    }
}
