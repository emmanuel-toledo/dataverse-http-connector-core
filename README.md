# Dataverse HTTP Connector Core

<img align="left" width="96" height="96" src="./Dataverse.png">

```Dataverse HTTP Connector Core``` library was created to help to the developers to create useful application connected to a Dataverse (or usually called Dynamics) environment in less time.

The package currently works using an ```Application User``` connection because this one has more request capacity. You only need to create a new ```App Registration``` resource in ```Microsoft Azure```, grant it permissions to connect to Dynamics and then register the ```Application User``` in ```Admin Power Platform``` portal.

## Getting Started

Let's see an example about how you can implement this library in an ```ASP.NET Core``` application.

### Configure connection

First of all make sure that you configure your connection inside one of the ```.json``` files. The configuration can be like the following.

```
"Dataverse": {
  "tenantId": "{ tenant unique identifier }",
  "clientId": "{ client unique identifier }",
  "clientSecret": "{ secret }",
  "resource": "{ dynamics url environment }",
  "connectionName": "{ custom identifier name }"
}
```

Each one of these values must be unique because you can configure multiple environments if you want.

```
"Dataverses": [
  {
    "tenantId": "{ tenant unique identifier }",
    "clientId": "{ client unique identifier }",
    "clientSecret": "{ secret }",
    "resource": "{ dynamics url environment }",
    "connectionName": "Org. #1"
  },
  {
    "tenantId": "{ tenant unique identifier }",
    "clientId": "{ client unique identifier }",
    "clientSecret": "{ secret }",
    "resource": "{ dynamics url environment }",
    "connectionName": "Org. #2"
  }
]
```

### Create your entity class

You also will need to create your custom classes that will be used to connect to ```Dataverse``` and use require context.

```
using Dataverse.Http.Connector.Core.Domains.Annotations;

[Entity("crmit_employee", "crmit_employees")]
public class Employees
{
    [Field("crmit_EmployeeId", "crmit_employeeid", FieldTypes.UniqueIdentifier)]
    public Guid Id { get; set; }

    [Field("crmit_Name", "crmit_name", FieldTypes.Text)]
    public string? Name { get; set; }

    [Field("crmit_EmployeeNumber", "crmit_employeenumber", FieldTypes.Text)]
    public string? EmployeeNumber { get; set; }

    [Field("CreatedOn", "createdon", FieldTypes.DateTime)]
    public DateTime CreatedOn { get; set; }

    [Field("ModifiedOn", "modifiedon", FieldTypes.DateTime)]
    public DateTime ModifiedOn { get; set; }

    [Field("statuscode", "statuscode", FieldTypes.OptionSet)]
    public int StatusCode { get; set; }

    [Field("statecode", "statecode", FieldTypes.OptionSet)]
    public int StateCode { get; set; }

    [Field("crmit_IsDeleted", "crmit_isdeleted", FieldTypes.BoolOptionSet)]
    public bool IsDeleted { get; set; }

    [Field("OwnerId", "ownerid", FieldTypes.Lookup, "systemusers")]
    public Guid OwnerId { get; set; }
}
```

As you can see in the previous example, you define your class and use ```Entity``` class to set ```Entity logical name``` and ```Entity collection logical name``` attributes.

To configure each property is pretty simple, you only need to use the class ```Field``` and set three or four values depending the field type in ```Dataverse```. The values for this attribute are:

- Property schema name.
- Property logical name.
- Property field type.
- Property linked entity collection logical name.

### Configure Dataverse service

Once that you have ready your configuration inside ```.json``` file and are created the ```entity classes``` you will need to call an extension method as you can see in the following code.

```
builder.Services.AddDataverseContext<DataverseContext>(builder =>
{
    builder.AddConnections(builder.Configuration.GetSection("Dataverses").Get<List<DataverseConnection>>()); // The default connection will be the first one.
    builder.SetDefaultConnection(
        builder.Configuration.GetSection("Dataverse").Get<DataverseConnection>()
    ); // You can set a default connection. If the connection does not exist, will add it.
    builder.AddEntitiesFromAssembly(typeof(Employees).Assembly); // You can add multiples entities definitions using assembly reference.
});
```

As you can see, with the new update you can use ```AddEntitiesFromAssembly``` method instead ```AddEntityDefinition<TEntity>()``` function.

The last you need to do is use this library.

### Dataverse service example

In your controller you need to use ```IDataverseContext``` service.

```
using Microsoft.AspNetCore.Mvc;
using Dataverse.Web.Api.Models;
using Dataverse.Http.Connector.Core.Persistence;
using Dataverse.Http.Connector.Core.Extensions.Utilities;

namespace Dataverse.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDataverseContext _dataverse;

        public EmployeesController(IDataverseContext dataverse)
            => _dataverse = dataverse;

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await _dataverse.Set<Employees>().ToListAsync();
            return Ok(employees);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedEmployee([FromQuery] int page, [FromQuery] int pageSize)
        {
            var pagedEmployees = await _dataverse.Set<Employees>().ToPagedListAsync(page, pageSize);
            return Ok(pagedEmployees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _dataverse.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.EmployeeId, id)).FirstOrDefaultAsync();
            if(employee is null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employees model)
        {
            await _dataverse.Set<Employees>().AddAsync(model);
            if (model.EmployeeId == Guid.Empty)
                return BadRequest();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employees model)
        {
            var employee = await _dataverse.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.EmployeeId, id)).FirstOrDefaultAsync();
            if (employee is null)
                return NotFound();

            model.EmployeeId = id;

            await _dataverse.Set<Employees>().UpdateAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _dataverse.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.EmployeeId, id)).FirstOrDefaultAsync();
            if (employee is null)
                return NotFound();
            await _dataverse.Set<Employees>().DeleteAsync(Employee);
            return Ok();
        }
    }
}
```

To add different conditions when you use a filter for a query,  it's needed to add the package ```using Dataverse.Http.Connector.Core.Extensions.Utilities``` to have access to different conditions like:

- Equal
- NotEqual
- Between
- NotBetween
- And much more...

This is a simple example about the implementation of this library. If any error during the request execution ocurred, automatically will throw an exception. 

To get more information about the exception you can access to the data named ```Content``` of the exception generated by this library.

```
try 
{

}
catch(Exception ex)
{
    var content = ex.Data["Content"];
}
```

If you configured many connection, you can change between those using a code as the following.

```
public async Task<IActionResult> GetEmployee(Guid id)
{
    var employee = await _dataverse.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.EmployeeId, id)).FirstOrDefaultAsync();
    if(employee is null) 
    {
        _dataverse.SetEnvironment($"Name, unique identifier or dataverse connection instance");
        employee = await _dataverse.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.EmployeeId, id)).FirstOrDefaultAsync();
    }
    return Ok(employee);
}
```

Now you have all what you need to start to use ```Dataverse HTTP Connector Core```.

## Considerations

This library currently does not support the ```link-entity``` join for a query request, but in future versions will be added this functionality. 

If you like this library, don't forget that you can support it if you want. This is an ```open source project```, and you are free if you want to help to improve it.

## Repository

- [Git repository](https://github.com/emmanuel-toledo/dataverse-http-connector-core)
- [NuGet package](https://www.nuget.org/packages/Dataverse.Http.Connector.Core)

## Authors

- [@emmanueltoledo](https://github.com/emmanuel-toledo)

