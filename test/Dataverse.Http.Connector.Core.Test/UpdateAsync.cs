namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class UpdateAsync
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
        public async Task Update_Existing_Custom_Entity_Record_In_Dataverse()
        {
            try
            {
                Employees employee = new()
                {
                    Id = new("fc5379ab-8530-ee11-bdf3-0022482dbd7a"),
                    Name = "Clark Kent",
                    EmployeeNumber = "EMP-002",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    StatusCode = 1,
                    StateCode = 0,
                    IsDeleted = true,
                    OwnerId = new("ef4269c5-a4f2-ec11-bb3d-00224820d6d5")
                };

                await _dataverse.Set<Employees>().UpdateAsync(employee);

                employee.Should().NotBe(null);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has ocurred during the update of {nameof(Employees)} record", ex);
            }
        }
    }
}
