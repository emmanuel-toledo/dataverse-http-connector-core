﻿namespace Dataverse.Http.Connector.Core.Test
{
    [TestClass]
    public class AddAsyncTest
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
        public async Task Create_New_Custom_Entity_Record_In_Dataverse()
        {
            try
            {
                Employees employee = new()
                {
                    Name = "Petter Parker",
                    EmployeeNumber = "EMP-005",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    StatusCode = 1,
                    StateCode = 0,
                    IsDeleted = false,
                    OwnerId = new("ef4269c5-a4f2-ec11-bb3d-00224820d6d5")
                };

                await _dataverse.Set<Employees>().AddAsync(employee);

                employee.Should().NotBe(null);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has ocurred during the creation of { nameof(Employees) } record", ex);
            }
        }
    }
}
