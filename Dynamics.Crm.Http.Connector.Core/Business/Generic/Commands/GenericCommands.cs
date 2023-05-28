using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Business.Generic.Commands
{
    internal class GenericCommands : IGenericCommands
    {
        public Task<HttpResponseMessage> CreateAsync(string schemaName, HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteAsync(string schemaName, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateAsync(string schemaName, Guid id, HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
