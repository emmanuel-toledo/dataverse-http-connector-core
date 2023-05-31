using Dynamics.Crm.Http.Connector.Core.Business.Commands;
using Dynamics.Crm.Http.Connector.Core.Business.Queries;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context;
using Dynamics.Crm.Http.Connector.Core.Extensions.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Business.Handler
{
    public interface IRequestHandler
    {
        Task<object> SendAsync(Action<Request> action);
    }

    public class RequestHandler : IRequestHandler
    {
        private Request _request = new();

        private readonly IDynamicsQueries _queries;

        private readonly IDynamicsCommands _commands;

        public RequestHandler(IDynamicsQueries queries, IDynamicsCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }

        public Task<object> SendAsync(Action<Request> action)
        {
            action(_request);
            var httpRequest = _request.ConvertToHttpRequest();
            throw new NotImplementedException();
        }
    }
}
