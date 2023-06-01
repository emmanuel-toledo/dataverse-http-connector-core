using Dynamics.Crm.Http.Connector.Core.Business.Commands;
using Dynamics.Crm.Http.Connector.Core.Business.Queries;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics;
using Dynamics.Crm.Http.Connector.Core.Domains.Dynamics.Context;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Extensions.Configurations;
using Dynamics.Crm.Http.Connector.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Crm.Http.Connector.Core.Business.Handler
{
    public interface IRequestHandler
    {
        Task<TEntity> SendAsync<TEntity>(Action<Request> action) where TEntity : class, new();

        Task<TEntity?> FirstOrDefaultAsync<TEntity>(Action<Request> action) where TEntity : class, new();

		Task<ICollection<TEntity>> ToListAsync<TEntity>(Action<Request> action) where TEntity : class, new();
	}

    public class RequestHandler : IRequestHandler
    {
        private readonly IDynamicsQueries _queries;

        private readonly IDynamicsCommands _commands;

        public RequestHandler(IDynamicsQueries queries, IDynamicsCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }

        public async Task<TEntity> SendAsync<TEntity>(Action<Request> action) where TEntity : class, new()
        {
   //         // Parse action to model.
   //         action(_request);
			//var httpRequest = _request.ConvertToHttpRequest();
			//// Evaluate method type.
			//switch (_request.Method)
   //         {
   //             case BaseMethod.FirstOrDefaultAsync:
   //                 break;
			//	case BaseMethod.ToListAsync:
			//		break;
			//	case BaseMethod.AddAsync:
			//		break;
			//	case BaseMethod.UpdateAzync:
			//		break;
			//	case BaseMethod.DeleteAzync:
			//		break;
   //             default:
   //                 throw new NotImplementedException("The selected base method was not implemented for the request.");
			//}
            
            throw new NotImplementedException();
        }
		private HttpRequestMessage GenerateRequest(Action<Request> action)
		{
			// Parse action to model.
			Request request = new();
			action(request);
			return request.ConvertToHttpRequest();
		}

        public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(Action<Request> action) where TEntity : class, new()
		{
			return await _queries.FirstOrDefaultAsync<TEntity>(GenerateRequest(action));
		}

		public async Task<ICollection<TEntity>> ToListAsync<TEntity>(Action<Request> action) where TEntity : class, new()
        {
			return await _queries.ToListAsync<TEntity>(GenerateRequest(action));
		}
	}
}
