using Dynamics.Crm.Http.Connector.Core.Business.Authentication;
using Dynamics.Crm.Http.Connector.Core.Domains.Builder;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Dynamics.Crm.Http.Connector.Core.Extensions;
using Dynamics.Crm.Http.Connector.Core.Extensions.Builders;
using Dynamics.Crm.Http.Connector.Core.Extensions.DependencyInjections;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder;
using Dynamics.Crm.Http.Connector.Core.Infrastructure.Builder.Options;
using Dynamics.Crm.Http.Connector.Core.Persistence;
using Dynamics.Crm.Http.Connector.Core.UT.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamics.Crm.Http.Connector.Core.UT
{
    [TestClass]
    public class EntityDbSetContact
    {
        private IServiceProvider _provider;

        private IServiceCollection _services;

        private IDynamicsBuilder _builder; // Dynamics Builder configuration (entites and connection information).

        private IDynamicsContext _context;

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
            foreach(var entity in _builder.Entities)
            {
                Console.WriteLine(entity.EntityType.Name);
            }
            _builder.Connection.ClientSecret = "New Secret"; // Transient service, each request of service will set initial setup.
            _builder = _provider.GetService<IDynamicsBuilder>()!;
            _context = _provider.GetService<IDynamicsContext>()!;
            Console.WriteLine(_builder.Entities.Count);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            try
            {
                var response = _context.Set<Contacts>()
                    .FilterAnd(conditions => conditions.NotNull(x => x.FullName))
                    .FilterOr(conditions =>
                    {
                        conditions.Equal(x => x.Id, new Guid("bfd14cf3-e939-ed11-9db1-000d3a990e03"));
                        conditions.Equal(x => x.Id, new Guid("c4d14cf3-e939-ed11-9db1-000d3a990e03"));
                    })
                    .Top(2)
                    .Distinct(true);
                Assert.IsTrue(!string.IsNullOrEmpty("Success"));
            } 
            catch(Exception ex)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
