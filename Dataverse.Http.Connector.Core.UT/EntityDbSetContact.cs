using Dataverse.Http.Connector.Core.Business.Authentication;
using Dataverse.Http.Connector.Core.Domains.Builder;
using Dataverse.Http.Connector.Core.Domains.Enums;
using Dataverse.Http.Connector.Core.Extensions;
using Dataverse.Http.Connector.Core.Extensions.DependencyInjections;
using Dataverse.Http.Connector.Core.Extensions.Utilities;
using Dataverse.Http.Connector.Core.Infrastructure.Builder;
using Dataverse.Http.Connector.Core.Infrastructure.Builder.Options;
using Dataverse.Http.Connector.Core.Persistence;
using Dataverse.Http.Connector.Core.UT.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Dataverse.Http.Connector.Core.UT
{
    [TestClass]
    public class EntityDbSetContact
    {
        private IServiceProvider _provider;

        private IServiceCollection _services;

        private IDataverseBuilder _builder; // Dynamics Builder configuration (entites and connection information).

        private IDataverseContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _services = new ServiceCollection();
            _services.AddDataverseContext<DataverseContext>(builder =>
            {
                builder.SetDefaultConnection(Dynamics.Connection); // Podemos user Bind desde IConfiguration para no crear nueva instancia directamente.
                builder.SetThrowExceptions(true);
                builder.AddEntityDeffinition<Contacts>();
            });
            _provider = _services.BuildServiceProvider();
            _builder = _provider.GetService<IDataverseBuilder>()!;
            foreach(var entity in _builder.Entities)
            {
                Console.WriteLine(entity.EntityType.Name);
            }
            //_builder.Connection.ClientSecret = "New Secret"; // Transient service, each request of service will set initial setup.
            _builder = _provider.GetService<IDataverseBuilder>()!;
            _context = _provider.GetService<IDataverseContext>()!;
            Console.WriteLine(_builder.Entities.Count);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            try
            {
                var contact = await _context.Set<Contacts>()
                    .FilterAnd(conditions => conditions.NotNull(x => x.FullName))
                    .FilterOr(conditions =>
                    {
                        conditions.In(x => x.Id, new Guid("bfd14cf3-e939-ed11-9db1-000d3a990e03"), new Guid("c4d14cf3-e939-ed11-9db1-000d3a990e03"));
                    })
                    .Top(2)
                    .Distinct(true)
                    .FirstOrDefaultAsync();

                var contacts = await _context.Set<Contacts>()
                    .FilterAnd(conditions => conditions.NotNull(x => x.FullName))
                    .FilterOr(conditions =>
                    {
                        conditions.Equal(x => x.Id, new Guid("bfd14cf3-e939-ed11-9db1-000d3a990e03"));
                        conditions.Equal(x => x.Id, new Guid("c4d14cf3-e939-ed11-9db1-000d3a990e03"));
                    })
                    .Top(2)
                    .Distinct(true)
                    .ToListAsync();
                Assert.IsTrue(!string.IsNullOrEmpty("Success"));
            } 
            catch(Exception ex)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
