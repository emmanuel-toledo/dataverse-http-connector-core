using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Facades.Generics.Queries
{
    /// <summary>
    /// This interface defines service to retrive entities deffinitions information.
    /// </summary>
    internal interface IEntitiesDeffinitions
    {
        /// <summary>
        /// Function to get Dynamics Entities deffinitions.
        /// </summary>
        /// <returns>Http response message object.</returns>
        Task<HttpResponseMessage> SendAsync();
    }

    /// <summary>
    /// This class implements service to retrive entities deffinitions information.
    /// </summary>
    internal class EntitiesDeffinitions : IEntitiesDeffinitions
    {
        /// <summary>
        /// Private Dynamics request service instance.
        /// </summary>
        private readonly IDynamicsRequest _dynamics;

        /// <summary>
        /// Initialize a new instance of "RetriveByOData" service.
        /// </summary>
        /// <param name="dynamics">Dynamics request service instance.</param>
        public EntitiesDeffinitions(IDynamicsRequest dynamics)
            => _dynamics = dynamics;

        /// <summary>
        /// Function to get Dynamics Entities deffinitions.
        /// </summary>
        /// <returns>Http response message object.</returns>
        public async Task<HttpResponseMessage> SendAsync()
            => await _dynamics.SendAsync(new HttpRequestMessage(method: HttpMethod.Get, "entities"), true);
    }
}
