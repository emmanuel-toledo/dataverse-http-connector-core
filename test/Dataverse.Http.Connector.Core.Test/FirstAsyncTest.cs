namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class FirstAsyncTest
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
        public async Task Get_Object_Instance_From_Custom_Entity_With_Equal_Filter()
        {
            try
            {
                Guid recordId = new("b37e2fee-e939-ed11-9db1-00224829a2ad");

                var contact = await _dataverse.Set<Contacts>()
                    .FilterAnd(conditions =>
                    {
                        conditions.Equal(x => x.Id, recordId);
                    })
                    .FirstAsync();

                contact.Should().NotBeNull();
            } 
            catch(Exception ex)
            {
                throw new Exception($"The record must exists inside dataverse's entity with name '{nameof(Contacts)}'.", ex);
            }
        }
    }
}
