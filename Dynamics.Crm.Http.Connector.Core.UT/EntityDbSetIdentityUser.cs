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
    public class EntityDbSetIdentityUser
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
                builder.SetThrowExceptions(true);
                builder.AddEntityDeffinition<Contacts>();
                builder.AddEntityDeffinition<IdentityUser>();
            });
            _provider = _services.BuildServiceProvider();
            _builder = _provider.GetService<IDynamicsBuilder>()!;
            foreach(var entity in _builder.Entities)
            {
                Console.WriteLine(entity.EntityType.Name);
            }
            //_builder.Connection.ClientSecret = "New Secret"; // Transient service, each request of service will set initial setup.
            _builder = _provider.GetService<IDynamicsBuilder>()!;
            _context = _provider.GetService<IDynamicsContext>()!;
            Console.WriteLine(_builder.Entities.Count);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            try
            {
                var contact = await _context.Set<IdentityUser>()
                    .FilterAnd(conditions => conditions.Equal(x => x.IdentityUserId, new Guid("d818df18-d600-ee11-8f6e-0022482dbd7a")))
                    .Distinct(true)
                    .FirstOrDefaultAsync();

                var contacts = await _context.Set<IdentityUser>()
                    .FilterAnd(conditions =>
                    {
                        conditions.In(x => x.IdentityUserId, new Guid("07ef9235-de00-ee11-8f6e-0022482db4d8"), new Guid("42ee683c-de00-ee11-8f6e-0022482db4d8"));
                    })
                    .Distinct(true)
                    .ToListAsync();

                contacts = await _context.Set<IdentityUser>()
                    .FilterAnd(conditions =>
                    {
                        conditions.NotIn(x => x.IdentityUserId, new Guid("07ef9235-de00-ee11-8f6e-0022482db4d8"), new Guid("42ee683c-de00-ee11-8f6e-0022482db4d8"));
                    })
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
