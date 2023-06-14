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
                builder.AddEntitiesFromAssembly(typeof(Employees).Assembly);
            });
            _provider = _services.BuildServiceProvider();
            _dataverse = _provider.GetService<IDataverseContext>()!;
        }

        [TestMethod]
        public async Task Get_Custom_Entity_Collection_As_Paged_Response()
        {
            try
            {
                var pagedResponse = await _dataverse.Set<Employees>().ToPagedListAsync(1, 5);

                pagedResponse.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred during the query to the dataverse's entity with name '{nameof(Employees)}'.", ex);
            }
        }
    }
}
