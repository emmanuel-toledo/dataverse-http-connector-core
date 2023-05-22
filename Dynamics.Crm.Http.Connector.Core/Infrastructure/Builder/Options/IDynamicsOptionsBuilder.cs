using Dynamics.Crm.Http.Connector.Core.Models.Builder;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options
{
    /// <summary>
    /// This interface defines methods and properties where we will find all the entities configurations to connect to Dynamics.
    /// </summary>
    public interface IDynamicsOptionsBuilder
    {
        /// <summary>
        /// Entity builders collection.
        /// </summary>
        ICollection<EntityBuilder> Entities { get; }

        /// <summary>
        /// Add a new entity builder reference deffinition.
        /// </summary>
        /// <typeparam name="TEntity">Entity class reference.</typeparam>
        void AddEntityDeffinition<TEntity>() where TEntity : class, new();
    }
}
