namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class FirstOrDefaultAsyncTest
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
        public async Task Get_Object_Instance_Or_Null_From_Custom_Entity_With_Equal_Filter()
        {
            try
            {
                Guid recordId = new("f20b6bf9-3b0a-ee11-8f6e-0022482db4d8");

                var employee = await _dataverse.Set<Employees>()
                    .FilterAnd(conditions => conditions.Equal(x => x.Id, recordId))
                    .FirstOrDefaultAsync();

                employee.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                throw new Exception($"The record must exists inside dataverse's entity with name '{nameof(Employees)}'.", ex);
            }
        }
    }
}
