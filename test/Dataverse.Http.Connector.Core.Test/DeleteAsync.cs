namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class DeleteAsyncTest
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
        public async Task Delete_Existing_Custom_Entity_Record_In_Dataverse()
        {
            try
            {
                Guid recordId = new("9d491127-8630-ee11-bdf3-0022482dbd7a");

                var employee = await _dataverse.Set<Employees>()
                    .FilterAnd(conditions => conditions.Equal(x => x.Id, recordId))
                    .FirstAsync();

                await _dataverse.Set<Employees>().DeleteAsync(employee);

                employee.Should().NotBe(null);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has ocurred during the deregistration of {nameof(Employees)} record", ex);
            }
        }
    }
}
