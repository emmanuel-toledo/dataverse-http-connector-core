using Dynamics.Crm.Http.Connector.Core.Models.Configurations;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;

namespace Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder
{
    /// <summary>
    /// This interface defines configuration, methods and properties to connect and transform data from Dynamics.
    /// </summary>
    public interface IDynamicsBuilder : IDynamicsOptionsBuilder
    {
        /// <summary>
        /// Throw exception flag.
        /// </summary>
        bool ThrowExceptions { get; }

        /// <summary>
        /// Dynamics connection object.
        /// </summary>
        DynamicsConnection? Connection { get; }

        /// <summary>
        /// Set a new Dynamics connection.
        /// </summary>
        /// <param name="connection">Dynamics connection object.</param>
        void SetConnection(DynamicsConnection connection);

        /// <summary>
        /// Set throw exceptions flag.
        /// </summary>
        /// <param name="throwExceptions">Throw exceptions flag.</param>
        void SetThrowExceptions(bool throwExceptions);
    }
}
