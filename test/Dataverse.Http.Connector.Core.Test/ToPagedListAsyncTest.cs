using System.Runtime.InteropServices;

namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class ToPagedListAsyncTest
    {
        private IServiceCollection _services;

        private IServiceProvider _provider;

        private IDataverseContext _dataverse;

        [TestInitialize]
        public void Initialize()
        {
            _services = new ServiceCollection();
            _services.AddDataverseContext<DataverseContext>(builder =>
            {
                builder.SetDefaultConnection(Conn.Dataverse.Connection);
                builder.SetThrowExceptions(true);
                builder.AddEntitiesFromAssembly(typeof(Employees));
            });
            _provider = _services.BuildServiceProvider();
            _dataverse = _provider.GetService<IDataverseContext>()!;
        }

        [TestMethod]
        public async Task Get_Custom_Entity_Collection_As_Paged_Response()
        {
            try
            {
                var pagedResponse = await _dataverse.Set<Employees>()
                    .FilterAnd(conditions =>
                    {
                        conditions.Equal(x => x.IsDeleted, false);
                        conditions.ThisYear(x => x.CreatedOn);
                        conditions.NotEqual(x => x.OwnerId, Guid.NewGuid());
                    })
                    .ToPagedListAsync(1, 2);

                pagedResponse.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred during the query to the dataverse's entity with name '{nameof(Contacts)}'.", ex);
            }
        }
    }
}
