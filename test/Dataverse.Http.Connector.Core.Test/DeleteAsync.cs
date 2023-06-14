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
        public async Task Delete_Existing_Employee_Record_In_Dataverse()
        {
            try
            {
                Guid recordId = new("448888da-700a-ee11-8f6e-0022482dbd7a");

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
