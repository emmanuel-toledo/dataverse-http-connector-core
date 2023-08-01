namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class ToListAsyncTest
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
                builder.AddEntitiesFromAssembly(typeof(Contacts));
            });
            _provider = _services.BuildServiceProvider();
            _dataverse = _provider.GetService<IDataverseContext>()!;
        }

        [TestMethod]
        public async Task Get_Custom_Entity_Collection_As_List()
        {
            try
            {
                var contacts = await _dataverse.Set<Contacts>()
                    .FilterAnd(conditions =>
                    {
                        conditions.NotEqual(c => c.Id, new Guid("b37e2fee-e939-ed11-9db1-00224829a2ad"));
                        conditions.Equal(c => c.StatusCode, 1);
                        conditions.Equal(c => c.StateCode, 0);
                        conditions.LessThan(c => c.CreatedOn, DateTime.Now);
                        conditions.Like(c => c.FirstName, "%ALEJANDRA%");
                    })
                    .Top(10)
                    .ToListAsync();

                contacts.Count.Should().BeGreaterThan(0);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred during the query to the dataverse's entity with name '{nameof(Contacts)}'.", ex);
            }
        }
    }
}
