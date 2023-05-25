using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Extensions.DependencyInjections;
using Dynamics.Crm.Http.Connector.Core.Facades.Requests;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;
using Dynamics.Crm.Http.Connector.Core.UT.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamics.Crm.Http.Connector.Core.UT
{
    [TestClass]
    public class EntitiesDeffinitions
    {
        private IServiceProvider _provider;

        private IServiceCollection _services;

        private IDynamicsRequest _request;

        private IDynamicsBuilder _builder; // Dynamics Builder configuration (entites and connection information).

        //private IDynamicsAuthenticator _authenticator; // Dynamics Authenticator service.


        [TestInitialize]
        public void Initialize()
        {
            _services = new ServiceCollection();
            _services.AddDynamicsContext<DynamicsContext>(builder =>
            {
                builder.SetDefaultConnection(Dynamics.Connection); // Podemos user Bind desde IConfiguration para no crear nueva instancia directamente.
                builder.SetThrowExceptions(false);
                builder.AddEntityDeffinition<Contacts>();
            });
            _provider = _services.BuildServiceProvider();
            _builder = _provider.GetService<IDynamicsBuilder>()!;

            //_builder.Connection.Resource = "https://dynamics.contoso.com";
            //_builder = _provider.GetService<IDynamicsBuilder>()!;

            //_authenticator = _provider.GetService<IDynamicsAuthenticator>()!;
            _request = _provider.GetService<IDynamicsRequest>()!;
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            try
            {
                var response = await _request.SendAsync(new HttpRequestMessage(HttpMethod.Get, "entities"));
                response = await _request.SendAsync(new HttpRequestMessage(HttpMethod.Get, "entities"));
                Assert.IsTrue(true);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
